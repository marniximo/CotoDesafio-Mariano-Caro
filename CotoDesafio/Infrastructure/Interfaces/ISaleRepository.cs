using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Asynchronously saves the specified sale to the data store.
        /// </summary>
        /// <param name="sale">The sale to be saved. Cannot be <see langword="null"/>.</param>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        Task SaveAsync(Sale sale);
    }
}
