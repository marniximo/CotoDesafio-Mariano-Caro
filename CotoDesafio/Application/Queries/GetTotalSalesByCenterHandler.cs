using CotoDesafio.Infrastructure.Interfaces;
using MediatR;

namespace CotoDesafio.Application.Queries
{
    public class GetTotalSalesByCenterHandler : IRequestHandler<GetTotalSalesByCenterQuery, SaleTotalDto>
    {
        private readonly ISaleReadRepository _saleRepository;

        public GetTotalSalesByCenterHandler(ISaleReadRepository _saleRepository)
        {
            this._saleRepository = _saleRepository;
        }

        public async Task<SaleTotalDto> Handle(GetTotalSalesByCenterQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetTotalSalesByCenterAsync(request.DistributionCenterId);
            return new SaleTotalDto (
                sales.Count, 
                sales.Sum(s => s.GetFinalSalePrice()) // calculo de impuesto en el total de la venta
            );
        }
    }
}
