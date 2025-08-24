using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface ICarModelReadRepository
    {
        /// <summary>
        /// Retrieves a car model that matches the specified model name.
        /// </summary>
        /// <param name="modelName">The name of the car model to search for. This value cannot be null or empty.</param>
        /// <returns>A <see cref="CarModel"/> object representing the car model if found; otherwise, <see langword="null"/>.</returns>
        Task<CarModel?> GetByModelAsync(string modelName);
    }
}
