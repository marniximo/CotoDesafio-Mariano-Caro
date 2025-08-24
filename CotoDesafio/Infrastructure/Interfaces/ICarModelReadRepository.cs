using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface ICarModelReadRepository
    {
        Task<CarModel?> GetByModelAsync(string modelName);
    }
}
