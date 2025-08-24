namespace CotoDesafio.Application.Commands
{
    public record RegisterSaleCommand(string CarModel, string CarChassisNumber, Guid CenterId); //Definicion del mensaje inmutable del command como record
}
