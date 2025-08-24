using CotoDesafio.Infrastructure.Interfaces;
using MediatR;

namespace CotoDesafio.Application.Queries
{
    public class GetTotalSalesHandler : IRequestHandler<GetTotalSalesQuery, IEnumerable<CenterSaleTotalDto>>
    {
        private readonly ISaleReadRepository _saleRepository;
        private readonly IDistributionCenterRepository _centerRepository;

        public GetTotalSalesHandler(ISaleReadRepository _saleRepository, IDistributionCenterRepository _centerRepository)
        {
            this._saleRepository = _saleRepository;
            this._centerRepository = _centerRepository;
        }

        public async Task<IEnumerable<CenterSaleTotalDto>> Handle(GetTotalSalesQuery request, CancellationToken cancellationToken)
        {
            var distributionCenters = await _centerRepository.GetAllAsync();
            var result = new List<CenterSaleTotalDto>();
            foreach (var center in distributionCenters)
            {
                var sales = await _saleRepository.GetTotalSalesByCenterAsync(center.Id);
                var totalSales = new CenterSaleTotalDto(
                    center.Id.ToString(),
                    center.Name,
                    new SaleTotalDto(sales.Count, sales.Sum(s => s.GetFinalSalePrice())) // calculo de impuesto en el total de la venta
                );
                result.Add(totalSales);
            }
            return result;
        }
    }
}
