using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CotoDesafio.Infrastructure.Repository
{
    public class DistributionCenterRepository : IDistributionCenterRepository
    {
        private readonly CarSalesDbContext _context;

        public DistributionCenterRepository(CarSalesDbContext _context)
        {
            this._context = _context;
        }

        public async Task<DistributionCenter?> GetByIdAsync(Guid id) => await _context.DistributionCenters.Where(d => d.Id == id).FirstOrDefaultAsync();
    }
}
