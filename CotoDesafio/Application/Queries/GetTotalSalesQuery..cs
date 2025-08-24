using CotoDesafio.Domain;
using MediatR;

namespace CotoDesafio.Application.Queries
{

    public record GetTotalSalesQuery()
        : IRequest<IEnumerable<CenterSaleTotalDto>>; // Se usa object porque las queries y commands en CQRS son inmutables. En este caso, definimos el mensaje GetSalesByCenterQuery que despues es manejado por el handler

    public record CenterSaleTotalDto(string DistributionCenterId, string DistributionCenterName, SaleTotalDto SaleTotal);
}