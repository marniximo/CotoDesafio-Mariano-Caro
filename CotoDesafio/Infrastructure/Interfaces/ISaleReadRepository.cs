using CotoDesafio.Domain;

namespace CotoDesafio.Infrastructure.Interfaces
{
    public interface ISaleReadRepository
    {
        /// <summary>
        /// Asynchronously retrieves the total sales for a specified sales center.
        /// </summary>
        /// <param name="centerId">The unique identifier of the sales center for which to retrieve sales data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Sale"/>
        /// objects  representing the sales associated with the specified sales center. The list will be empty if no
        /// sales are found.</returns>
        Task<List<Sale>> GetTotalSalesByCenterAsync(Guid centerId);

        /// <summary>
        /// Asynchronously retrieves the total count of sales.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total number of sales.</returns>
        Task<int> GetTotalSalesCount();
        
        /// <summary>
        /// Retrieves the sale information associated with the specified car chassis number.
        /// </summary>
        /// <param name="chassisNumber">The unique chassis number of the car for which the sale information is to be retrieved. This value cannot be
        /// null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the  <see cref="Sale"/> object
        /// associated with the specified chassis number, or <see langword="null"/>  if no sale is found.</returns>
        Task<Sale?> GetSaleByCarChassisNumber(string chassisNumber);
    }
}
