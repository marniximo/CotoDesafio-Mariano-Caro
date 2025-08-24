namespace CotoDesafio.Domain
{
    //Se agrega soporte para pesos o dolares
    public record Money(decimal Amount, string Currency)
    {
        //Calculo de Impuesto, usado para el impuesto al auto deportivo
        public Money AddTax(decimal taxPercentage)
        {
            return new Money(Amount * (1 + taxPercentage / 100), Currency);
        }
    }
}
