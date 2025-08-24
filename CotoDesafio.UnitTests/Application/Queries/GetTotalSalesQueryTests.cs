using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CotoDesafio.Application.Queries;
using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace CotoDesafio.UnitTests.Application.Queries
{
    public class GetTotalSalesQueryTests
    {
        private readonly Mock<ISaleReadRepository> _saleRepoMock = new();
        private readonly Mock<IDistributionCenterRepository> _centerRepoMock = new();
        private readonly GetTotalSalesHandler _handler;

        public GetTotalSalesQueryTests()
        {
            _handler = new GetTotalSalesHandler(_saleRepoMock.Object, _centerRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsTotalsForAllCenters()
        {
            // Arrange
            var center1 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center1" };
            var center2 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center2" };
            var centers = new List<DistributionCenter> { center1, center2 };

            var sales1 = new List<Sale>
            {
                new Sale { CarModel = new CarModel { Price = 100m }, CarModelName = "A" },
                new Sale { CarModel = new CarModel { Price = 200m }, CarModelName = "B" }
            };
            var sales2 = new List<Sale>
            {
                new Sale { CarModel = new CarModel { Price = 300m }, CarModelName = "C" }
            };

            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(centers);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center1.Id)).ReturnsAsync(sales1);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center2.Id)).ReturnsAsync(sales2);

            // Act
            var result = (await _handler.Handle(new GetTotalSalesQuery(), CancellationToken.None)).ToList();

            // Assert
            Assert.Equal(2, result.Count);

            var dto1 = result.First(r => r.DistributionCenterId == center1.Id.ToString());
            Assert.Equal("Center1", dto1.DistributionCenterName);
            Assert.Equal(2, dto1.SaleTotal.SaleAmount);
            Assert.Equal(300, dto1.SaleTotal.SaleTotal);

            var dto2 = result.First(r => r.DistributionCenterId == center2.Id.ToString());
            Assert.Equal("Center2", dto2.DistributionCenterName);
            Assert.Equal(1, dto2.SaleTotal.SaleAmount);
            Assert.Equal(300, dto2.SaleTotal.SaleTotal);
        }

        [Fact]
        public async Task Handle_ReturnsEmpty_WhenNoCenters()
        {
            // Arrange
            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<DistributionCenter>());

            // Act
            var result = await _handler.Handle(new GetTotalSalesQuery(), CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_ReturnsZeroTotals_WhenNoSalesForCenter()
        {
            // Arrange
            var center = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center1" };
            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<DistributionCenter> { center });
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center.Id)).ReturnsAsync(new List<Sale>());

            // Act
            var result = (await _handler.Handle(new GetTotalSalesQuery(), CancellationToken.None)).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal(center.Id.ToString(), result[0].DistributionCenterId);
            Assert.Equal("Center1", result[0].DistributionCenterName);
            Assert.Equal(0, result[0].SaleTotal.SaleAmount);
            Assert.Equal(0, result[0].SaleTotal.SaleTotal);
        }

        [Fact]
        public async Task Handle_CallsRepositoryWithCorrectIds()
        {
            // Arrange
            var center1 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center1" };
            var center2 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center2" };
            var centers = new List<DistributionCenter> { center1, center2 };

            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(centers);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Sale>());

            // Act
            await _handler.Handle(new GetTotalSalesQuery(), CancellationToken.None);

            // Assert
            _saleRepoMock.Verify(r => r.GetTotalSalesByCenterAsync(center1.Id), Times.Once);
            _saleRepoMock.Verify(r => r.GetTotalSalesByCenterAsync(center2.Id), Times.Once);
        }
    }
}