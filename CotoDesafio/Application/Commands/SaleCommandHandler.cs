using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CotoDesafio.Application.Commands
{
    public class SaleCommandHandler : IRequestHandler<RegisterSaleCommand, Sale>
    {
        private readonly ICarModelReadRepository _carModelRepo;
        private readonly IDistributionCenterRepository _centerRepo;
        private readonly ISaleRepository _saleRepo;
        private readonly ISaleReadRepository _saleReadRepo;

        // Se usa inyecciond de dependencias para resolver las dependencias que esta implementacion necesita
        public SaleCommandHandler(ICarModelReadRepository carModelRepo, IDistributionCenterRepository centerRepo, ISaleRepository saleRepo, ISaleReadRepository saleReadRepo)
        {
            _carModelRepo = carModelRepo;
            _centerRepo = centerRepo;
            _saleRepo = saleRepo;
            _saleReadRepo = saleReadRepo;
        }

        public async Task<Sale> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
        {
            var existingSale = await _saleReadRepo.GetSaleByCarChassisNumber(command.CarChassisNumber);
            if (existingSale != null) throw new ValidationException("Sale of chassis number already registered");

            var carModel = await _carModelRepo.GetByModelAsync(command.CarModel);
            var center = await _centerRepo.GetByIdAsync(command.CenterId);

            if (carModel == null) throw new KeyNotFoundException("Car Model not found");
            if (center == null) throw new KeyNotFoundException("Center not found");

            var sale = new Sale
            {
                CarChassisNumber = command.CarChassisNumber,
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
