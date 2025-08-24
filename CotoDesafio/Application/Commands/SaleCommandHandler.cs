using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;

namespace CotoDesafio.Application.Commands
{
    public class SaleCommandHandler
    {
        private readonly ICarModelReadRepository _carModelRepo;
        private readonly IDistributionCenterRepository _centerRepo;
        private readonly ISaleRepository _saleRepo;

        // Se usa inyecciond de dependencias para resolver las dependencias que esta implementacion necesita
        public SaleCommandHandler(ICarModelReadRepository carModelRepo, IDistributionCenterRepository centerRepo, ISaleRepository saleRepo)
        {
            _carModelRepo = carModelRepo;
            _centerRepo = centerRepo;
            _saleRepo = saleRepo;
        }

        public async Task<Sale> Handle(RegisterSaleCommand command)
        {
            var carModel = await _carModelRepo.GetByModelAsync(command.CarModel);
            var center = await _centerRepo.GetByIdAsync(command.CenterId);

            if (carModel == null) throw new Exception("Car Model not found");
            if (center == null) throw new Exception("Center not found");

            var sale = new Sale
            {
                CarChassisNumber = carModel.CarChassisNumber,
                CarModelName = carModel.CarModelName,
                DistributionCenterId = command.CenterId,
                Date = DateTime.Now,
                CarModel = carModel,
                DistributionCenter = center,
            };
            await _saleRepo.SaveAsync(sale);
            return sale;
        }
    }
}
