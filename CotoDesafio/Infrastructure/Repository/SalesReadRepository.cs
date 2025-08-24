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

        public async Task<List<Sale>> GetTotalSalesByCenterAsync(Guid centerId) =>
            await context.Sales.Where(s => s.DistributionCenterId == centerId).Include(s => s.DistributionCenter).ToListAsync();

        public async Task<int> GetTotalSalesCount() => await context.Sales.CountAsync();
    }
}
