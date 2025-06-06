using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using YourTrips.Application.DTOs.Route;
using YourTrips.Application.DTOs.Route.PartRoutes;
using YourTrips.Application.Interfaces.GoogleMaps;
using YourTrips.Infrastructure.Data;
using YourTrips.Infrastructure.Services.Routes;

[TestFixture]
public class RouteServiceTests
{
    private YourTripsDbContext _context;
    private IMapper _mapper;
    private RouteService _service;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<YourTripsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new YourTripsDbContext(options);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<YourTrips.Core.Entities.Route, RouteDto>();
        });
        _mapper = config.CreateMapper();

        var googleServiceMock = new Mock<IGooglePlacesService>();

        _service = new RouteService(_context, _mapper, googleServiceMock.Object);
    }
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
    [Test]
    public async Task CreateRouteAsync_Should_Create_Route_And_Return_Dto()
    {
        var dto = new CreateRouteDto { Name = "Test Route" };
        var userId = Guid.NewGuid();

        var result = await _service.CreateRouteAsync(dto, userId);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Data.Name, Is.EqualTo(dto.Name));
        Assert.That(result.Data, Is.Not.Null);
    }

    [Test]
    public async Task DeleteRouteAsync_Should_Remove_Existing_Route()
    {
        var route = new YourTrips.Core.Entities.Route { Name = "Delete Me", UserId = Guid.NewGuid() };
        _context.Routes.Add(route);
        await _context.SaveChangesAsync();

        var result = await _service.DeleteRouteAsync(route.Id);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Message, Is.EqualTo("Маршрут успішно видалено"));
    }
}
