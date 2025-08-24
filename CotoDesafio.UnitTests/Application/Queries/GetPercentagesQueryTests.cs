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
    public class GetPercentagesQueryTests
    {
        private readonly Mock<ISaleReadRepository> _saleRepoMock = new();
        private readonly Mock<IDistributionCenterRepository> _centerRepoMock = new();

        private readonly GetPercentagesHandler _handler;

        public GetPercentagesQueryTests()
        {
            _handler = new GetPercentagesHandler(_saleRepoMock.Object, _centerRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsPercentagesByCenter_WhenDataExists()
        {
            // Arrange
            var center1 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center1" };
            var center2 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center2" };
            var centers = new List<DistributionCenter> { center1, center2 };

            var salesCenter1 = new List<Sale>
            {
                new Sale { CarModelName = "ModelA" },
                new Sale { CarModelName = "ModelA" },
                new Sale { CarModelName = "ModelB" }
            };
            var salesCenter2 = new List<Sale>
            {
                new Sale { CarModelName = "ModelA" },
                new Sale { CarModelName = "ModelC" }
            };

            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(centers);
            _saleRepoMock.Setup(r => r.GetTotalSalesCount()).ReturnsAsync(5);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center1.Id)).ReturnsAsync(salesCenter1);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center2.Id)).ReturnsAsync(salesCenter2);

            // Act
            var result = (await _handler.Handle(new GetPercentagesQuery(), CancellationToken.None)).ToList();

            // Assert
            Assert.Equal(2, result.Count);

            var center1Result = result.First(r => r.DistributionCenterId == center1.Id.ToString());
            Assert.Equal("Center1", center1Result.DistributionCenterName);
            Assert.Equal(2, center1Result.PercentageByModel.Count);
            Assert.Contains(center1Result.PercentageByModel, m => m.CarModel == "ModelA" && m.SalesTotalPercentage == 40m);
            Assert.Contains(center1Result.PercentageByModel, m => m.CarModel == "ModelB" && m.SalesTotalPercentage == 20m);

            var center2Result = result.First(r => r.DistributionCenterId == center2.Id.ToString());
            Assert.Equal("Center2", center2Result.DistributionCenterName);
            Assert.Equal(2, center2Result.PercentageByModel.Count);
            Assert.Contains(center2Result.PercentageByModel, m => m.CarModel == "ModelA" && m.SalesTotalPercentage == 20m);
            Assert.Contains(center2Result.PercentageByModel, m => m.CarModel == "ModelC" && m.SalesTotalPercentage == 20m);
        }

        [Fact]
        public async Task Handle_ReturnsEmpty_WhenNoCenters()
        {
            // Arrange
            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<DistributionCenter>());
            _saleRepoMock.Setup(r => r.GetTotalSalesCount()).ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(new GetPercentagesQuery(), CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_ReturnsZeroPercentages_WhenNoSales()
        {
            // Arrange
            var center = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center1" };
            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<DistributionCenter> { center });
            _saleRepoMock.Setup(r => r.GetTotalSalesCount()).ReturnsAsync(0);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center.Id)).ReturnsAsync(new List<Sale>());

            // Act
            var result = (await _handler.Handle(new GetPercentagesQuery(), CancellationToken.None)).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal(center.Id.ToString(), result[0].DistributionCenterId);
            Assert.Empty(result[0].PercentageByModel);
        }

        [Fact]
        public async Task Handle_HandlesMultipleModelsAndCenters()
        {
            // Arrange
            var center1 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center1" };
            var center2 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center2" };
            var centers = new List<DistributionCenter> { center1, center2 };

            var salesCenter1 = new List<Sale>
            {
                new Sale { CarModelName = "ModelA" },
                new Sale { CarModelName = "ModelB" }
            };
            var salesCenter2 = new List<Sale>
            {
                new Sale { CarModelName = "ModelB" },
                new Sale { CarModelName = "ModelC" },
                new Sale { CarModelName = "ModelC" }
            };

            _centerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(centers);
            _saleRepoMock.Setup(r => r.GetTotalSalesCount()).ReturnsAsync(5);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center1.Id)).ReturnsAsync(salesCenter1);
            _saleRepoMock.Setup(r => r.GetTotalSalesByCenterAsync(center2.Id)).ReturnsAsync(salesCenter2);

            // Act
            var result = (await _handler.Handle(new GetPercentagesQuery(), CancellationToken.None)).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.DistributionCenterId == center1.Id.ToString());
            Assert.Contains(result, r => r.DistributionCenterId == center2.Id.ToString());
        }
    }
}