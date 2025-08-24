using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface IDistributionCenterRepository
    {
        /// <summary>
        /// Retrieves a distribution center by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the distribution center to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the  <see
        /// cref="DistributionCenter"/> if found; otherwise, <see langword="null"/>.</returns>
        Task<DistributionCenter?> GetByIdAsync(Guid id);

        /// <summary>
        /// Asynchronously retrieves a list of all distribution centers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of  <see
        /// cref="DistributionCenter"/> objects representing all distribution centers. The list  will be empty if no
        /// distribution centers are found.</returns>
        Task<List<DistributionCenter>> GetAllAsync();
    }
}
