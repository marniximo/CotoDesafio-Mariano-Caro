using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface ISaleRepository
    {
        Task SaveAsync(Sale sale);
    }
}
