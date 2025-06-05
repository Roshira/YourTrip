using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.Route;
using YourTrips.Core.DTOs;
using YourTrips.Core.DTOs.Route.PartRoutes;

namespace YourTrips.Core.Interfaces.Routes
{
    /// <summary>
    /// Interface for managing routes, including creating, updating, deleting, and retrieving routes.
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Creates a new route for the specified user.
        /// </summary>
        /// <param name="dto">Data transfer object containing route creation data.</param>
        /// <param name="userId">The ID of the user creating the route.</param>
        /// <returns>A result DTO containing the created route data.</returns>
        Task<ResultDto<RouteDto>> CreateRouteAsync(CreateRouteDto dto, Guid userId);

        /// <summary>
        /// Deletes a route by its identifier.
        /// </summary>
        /// <param name="routeId">The identifier of the route to delete.</param>
        /// <returns>A result DTO indicating success or failure.</returns>
        Task<ResultDto> DeleteRouteAsync(int routeId);

        /// <summary>
        /// Retrieves all routes for a specified user.
        /// </summary>
        /// <param name="userId">The user ID whose routes to retrieve.</param>
        /// <returns>A result DTO containing a list of route DTOs.</returns>
        Task<ResultDto<List<RouteDto>>> GetAllUserRoutesAsync(Guid userId);

        /// <summary>
        /// Updates the image URL for a specific route.
        /// </summary>
        /// <param name="imageUrl">The new image URL to set.</param>
        /// <param name="routeId">The route identifier to update.</param>
        /// <returns>A result DTO indicating success or failure.</returns>
        Task<ResultDto> UpdateImageAsync(string imageUrl, int routeId);

        /// <summary>
        /// Retrieves detailed information for displaying a route.
        /// </summary>
        /// <param name="routeId">The identifier of the route to display.</param>
        /// <returns>A DTO with detailed route information for display.</returns>
        Task<ShowRouteDto> ShowRouteAsync(int routeId);
    }
}
