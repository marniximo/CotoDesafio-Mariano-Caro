using CotoDesafio.Infrastructure.Interfaces;
using MediatR;

namespace CotoDesafio.Application.Queries
{
    public class GetPercentagesHandler : IRequestHandler<GetPercentagesQuery, IEnumerable<PercentageByCenterDto>>
    {
        private readonly ISaleReadRepository _saleRepository;
        private readonly IDistributionCenterRepository _centerRepository;

        public GetPercentagesHandler(ISaleReadRepository _saleRepository, IDistributionCenterRepository _centerRepository)
        {
            this._saleRepository = _saleRepository;
            this._centerRepository = _centerRepository;
        }

        // Este metodo calcula el porcentaje de ventas por modelo en cada centro de distribucion con respecto al total de ventas en unidades de la empresa
        public async Task<IEnumerable<PercentageByCenterDto>> Handle(GetPercentagesQuery request, CancellationToken cancellationToken)
        {
            var distributionCenters = await _centerRepository.GetAllAsync(); // obtengo todos los centros de distribucion
            var totalSalesCount = await _saleRepository.GetTotalSalesCount(); // obtengo el total de ventas en unidades de la empresa
            var result = new List<PercentageByCenterDto>();
            foreach (var center in distributionCenters)
            {
                var sales = await _saleRepository.GetTotalSalesByCenterAsync(center.Id); // obtengo todas las ventas del centro de distribucion
                var modelsGrouped = sales.GroupBy(s => s.CarModelName)
                                  .Select(g => new PercentageByModelDto(
                                      g.Key,
                                      Math.Round((decimal)g.Count() / totalSalesCount * 100, 2) // porcentaje de ventas por modelo
                                  )).ToList(); // agrupo las ventas por modelo de este centro y calculo el porcentaje con respecto al total de ventas en unidades de la empresa
                var totalSales = new PercentageByCenterDto(
                    center.Id.ToString(),
                    center.Name,
                    modelsGrouped
                );
                result.Add(totalSales);
            }
            return result;
        }
    }
}
