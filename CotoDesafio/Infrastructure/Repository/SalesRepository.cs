using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;

namespace CotoDesafio.Infrastructure.Repository
{
    public class SalesRepository : ISaleRepository
    {
        private readonly CarSalesDbContext context;

        public SalesRepository(CarSalesDbContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync(Sale sale)
        {
            await context.Sales.AddAsync(sale);
        }
    }
}
