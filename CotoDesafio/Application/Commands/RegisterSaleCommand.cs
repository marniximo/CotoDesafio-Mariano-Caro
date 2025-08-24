using CotoDesafio.Domain;
using MediatR;

namespace CotoDesafio.Application.Commands
{
    public record RegisterSaleCommand(string CarModel, string CarChassisNumber, Guid CenterId): IRequest<Sale>; //Definicion del mensaje inmutable del command como record
}
