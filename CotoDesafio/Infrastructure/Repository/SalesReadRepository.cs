using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CotoDesafio.Infrastructure.Repository
{
    public class SalesReadRepository : ISaleReadRepository
    {
        private readonly CarSalesDbContext context;

        public SalesReadRepository(CarSalesDbContext context)
        {
            this.context = context;
        }

        public async Task<Sale?> GetSaleByCarChassisNumber(string chassisNumber) => 
            await context.Sales.Where(s => s.CarChassisNumber == chassisNumber)
                .Include(s => s.CarModel)
                .Include(s => s.DistributionCenter)
                .FirstOrDefaultAsync();

        public async Task<List<Sale>> GetTotalSalesByCenterAsync(Guid centerId) =>
            await context.Sales.Where(s => s.DistributionCenterId == centerId)
                .Include(s => s.CarModel)
                .Include(s => s.DistributionCenter)
                .ToListAsync();

        public async Task<int> GetTotalSalesCount() => await context.Sales.CountAsync();
    }
}
