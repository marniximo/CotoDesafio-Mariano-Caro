using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface IDistributionCenterRepository
    {
        Task<DistributionCenter?> GetByIdAsync(Guid id);
        Task<List<DistributionCenter>> GetAllAsync();
    }
}
