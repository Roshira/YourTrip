using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.DTOs;

namespace YourTrips.Web.Extensions
{
    /// <summary>
    /// Extension methods for ASP.NET Core controllers and claims.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Converts a non-generic <see cref="ResultDto"/> to an <see cref="IActionResult"/> response.
        /// Returns 200 OK if successful, otherwise 400 Bad Request.
        /// </summary>
        /// <param name="result">The result DTO to convert.</param>
        /// <returns>An appropriate <see cref="IActionResult"/>.</returns>
        public static IActionResult ToApiResponse(this ResultDto result)
        {
            return result.IsSuccess
                ? (IActionResult)new OkObjectResult(result)
                : new BadRequestObjectResult(result);
        }

        /// <summary>
        /// Converts a generic <see cref="ResultDto{T}"/> to an <see cref="IActionResult"/> response.
        /// Returns 200 OK if successful, otherwise 400 Bad Request.
        /// </summary>
        /// <typeparam name="T">The type of data in the result.</typeparam>
        /// <param name="result">The generic result DTO to convert.</param>
        /// <returns>An appropriate <see cref="IActionResult"/>.</returns>
        public static IActionResult ToApiResponse<T>(this ResultDto<T> result)
        {
            return result.IsSuccess
                ? (IActionResult)new OkObjectResult(result)
                : new BadRequestObjectResult(result);
        }

        /// <summary>
        /// Extracts the current authenticated user's ID from claims.
        /// </summary>
        /// <param name="user">The claims principal (usually from the controller's User property).</param>
        /// <returns>The user's GUID.</returns>
        public static Guid GetUserId(this System.Security.Claims.ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
    }
}
