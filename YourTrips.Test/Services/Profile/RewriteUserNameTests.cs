using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YourTrips.Core.Entities;
using YourTrips.Infrastructure.Services.ProfileServices;
using System.Collections.Generic;
using System.Linq;

namespace YourTrips.Tests.Services
{
    /// <summary>
    /// Profile tests
    /// </summary>
    [TestFixture]
    public class RewriteUserNameTests
    {
        private Mock<UserManager<User>> _userManagerMock;
        private RewriteUserName _service;
        private User _testUser;

        [SetUp]
        public void SetUp()
        {
            var store = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _service = new RewriteUserName(_userManagerMock.Object);

            _testUser = new User { UserName = "OldUser" };
        }

        [Test]
        public async Task RewriteUserNameAsync_ShouldReturnFail_WhenUserIsNull()
        {
            // Arrange
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _service.RewriteUserNameAsync(new ClaimsPrincipal(), "NewUser");

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Message, Is.EqualTo("User not found for the current session."));
        }

        [Test]
        public async Task RewriteUserNameAsync_ShouldReturnFail_WhenUsernameAlreadyExists()
        {
            // Arrange
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(_testUser);

            _userManagerMock.Setup(um => um.FindByNameAsync("NewUser"))
                .ReturnsAsync(new User());

            // Act
            var result = await _service.RewriteUserNameAsync(new ClaimsPrincipal(), "NewUser");

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Message, Is.EqualTo("Username NewUser already exists"));
        }

        [Test]
        public async Task RewriteUserNameAsync_ShouldReturnFail_WhenUpdateFails()
        {
            // Arrange
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(_testUser);

            _userManagerMock.Setup(um => um.FindByNameAsync("NewUser"))
                .ReturnsAsync((User)null);

            _userManagerMock.Setup(um => um.NormalizeName("NewUser"))
                .Returns("NEWUSER");

            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed" }));

            // Act
            var result = await _service.RewriteUserNameAsync(new ClaimsPrincipal(), "NewUser");

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Message, Is.EqualTo("Failed to update username"));
            Assert.That(result.Errors, Is.Not.Null);
            Assert.That(result.Errors.First(), Is.EqualTo("Update failed"));
        }

        [Test]
        public async Task RewriteUserNameAsync_ShouldReturnSuccess_WhenUpdateIsSuccessful()
        {
            // Arrange
            _userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(_testUser);

            _userManagerMock.Setup(um => um.FindByNameAsync("NewUser"))
                .ReturnsAsync((User)null);

            _userManagerMock.Setup(um => um.NormalizeName("NewUser"))
                .Returns("NEWUSER");

            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _service.RewriteUserNameAsync(new ClaimsPrincipal(), "NewUser");

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Message, Is.EqualTo("UserName successfully updated"));
            Assert.That(_testUser.UserName, Is.EqualTo("NewUser"));
            Assert.That(_testUser.NormalizedUserName, Is.EqualTo("NEWUSER"));
        }
    }
}
