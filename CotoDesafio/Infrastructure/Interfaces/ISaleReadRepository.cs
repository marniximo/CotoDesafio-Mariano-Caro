using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface ISaleReadRepository
    {
        Task<List<Sale>> GetTotalSalesByCenterAsync(Guid centerId);
        Task<int> GetTotalSalesCount();
        Task<Sale?> GetSaleByCarChassisNumber(string chassisNumber);
    }
}
