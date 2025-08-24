using CotoDesafio.Application.Queries;
using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CotoDesafio.UnitTests.Application.Queries
{
    public class GetTotalSalesByCenterQueryTests
    {
        [Fact]
        public async Task Handle_ReturnsCorrectSaleTotalDto_WhenSalesExist()
        {
            // Arrange
            var centerId = Guid.NewGuid();
            var sales = new List<Sale>
            {
                new Sale{ CarModel = new CarModel { Price = 100m }, CarModelName = "A" },
                new Sale{ CarModel = new CarModel { Price = 200m }, CarModelName = "B" },
            };

            var saleRepoMock = new Mock<ISaleReadRepository>();
            saleRepoMock
                .Setup(r => r.GetTotalSalesByCenterAsync(centerId))
                .ReturnsAsync(sales);

            var handler = new GetTotalSalesByCenterHandler(saleRepoMock.Object);
            var query = new GetTotalSalesByCenterQuery ( centerId );

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.SaleAmount);
            Assert.Equal(300m, result.SaleTotal);
        }

        [Fact]
        public async Task Handle_ReturnsZero_WhenNoSalesExist()
        {
            // Arrange
            var centerId = Guid.NewGuid();
            var sales = new List<Sale>();

            var saleRepoMock = new Mock<ISaleReadRepository>();
            saleRepoMock
                .Setup(r => r.GetTotalSalesByCenterAsync(centerId))
                .ReturnsAsync(sales);

            var handler = new GetTotalSalesByCenterHandler(saleRepoMock.Object);
            var query = new GetTotalSalesByCenterQuery(centerId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(0, result.SaleAmount);
            Assert.Equal(0m, result.SaleTotal);
        }

        [Fact]
        public async Task Handle_CallsRepositoryWithCorrectCenterId()
        {
            // Arrange
            var centerId = Guid.NewGuid();
            var saleRepoMock = new Mock<ISaleReadRepository>();
            saleRepoMock
                .Setup(r => r.GetTotalSalesByCenterAsync(centerId))
                .ReturnsAsync(new List<Sale>());

            var handler = new GetTotalSalesByCenterHandler(saleRepoMock.Object);
            var query = new GetTotalSalesByCenterQuery (centerId);

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            saleRepoMock.Verify(r => r.GetTotalSalesByCenterAsync(centerId), Times.Once);
        }
    }
}