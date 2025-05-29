using Microsoft.AspNetCore.Mvc;
using YourTrips.Core.DTOs;

namespace YourTrips.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToApiResponse(this ResultDto result)
        {
            return result.IsSuccess
     ? (IActionResult)new OkObjectResult(result)
     : new BadRequestObjectResult(result);
        }

        public static IActionResult ToApiResponse<T>(this ResultDto<T> result)
        {
            return result.IsSuccess
     ? (IActionResult)new OkObjectResult(result)
     : new BadRequestObjectResult(result);
        }

        public static Guid GetUserId(this System.Security.Claims.ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        }
    }
}