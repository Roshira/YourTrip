using System.Threading.Tasks;
using YourTrips.Application.DTOs;
using YourTrips.Application.DTOs.Route.Saved;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    /// <summary>
    /// Interface for saving and deleting JSON data related to saved route entities.
    /// </summary>
    public interface ISavDelJSONModel
    {
        /// <summary>
        /// Saves a JSON representation of a saved entity asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the saved entity implementing <see cref="ISavedEntity"/>.</typeparam>
        /// <param name="savedDto">The DTO containing the JSON data to save.</param>
        /// <returns>A <see cref="ResultDto"/> representing the operation result.</returns>
        Task<ResultDto> SaveJsonAsync<T>(SavedDto savedDto) where T : class, ISavedEntity, new();

        /// <summary>
        /// Deletes a saved entity asynchronously based on the provided delete DTO.
        /// </summary>
        /// <typeparam name="T">The type of the saved entity implementing <see cref="ISavedEntity"/>.</typeparam>
        /// <param name="deleteSavedDto">The DTO containing information needed to identify and delete the saved entity.</param>
        /// <returns>A <see cref="ResultDto"/> representing the operation result.</returns>
        Task<ResultDto> DeleteJsonAsync<T>(DeleteSavedDto deleteSavedDto) where T : class, ISavedEntity, new();
    }
}
