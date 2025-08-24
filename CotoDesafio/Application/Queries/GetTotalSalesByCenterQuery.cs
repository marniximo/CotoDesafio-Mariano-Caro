using CotoDesafio.Domain;
using MediatR;

namespace CotoDesafio.Application.Queries
{

    public record GetTotalSalesByCenterQuery(Guid DistributionCenterId)
        : IRequest<SaleTotalDto>; // Se usa object porque las queries y commands en CQRS son inmutables. En este caso, definimos el mensaje GetSalesByCenterQuery que despues es manejado por el handler

    public record SaleTotalDto(int SaleAmount, decimal SaleTotal); // El DTO tambien es un record porque solo transporta datos y es inmutable
}