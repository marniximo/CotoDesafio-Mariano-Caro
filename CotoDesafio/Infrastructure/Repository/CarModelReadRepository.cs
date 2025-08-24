using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CotoDesafio.Infrastructure.Repository
{
    public class CarModelReadRepository : ICarModelReadRepository
    {
        private readonly CarSalesDbContext _context;

        public CarModelReadRepository(CarSalesDbContext _context)
        {
            this._context = _context;
        }

        public async Task<CarModel?> GetByModelAsync(string modelName) => await _context.CarModels.Where(c => c.CarModelName == modelName).FirstOrDefaultAsync();
    }
}
