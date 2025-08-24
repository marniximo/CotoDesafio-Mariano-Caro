using CotoDesafio.Application.Commands;
using CotoDesafio.Domain;
using CotoDesafio.Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotoDesafio.UnitTests.Application.Commands
{
    public class SaleCommandHandlerTests
    {
        private readonly Mock<ICarModelReadRepository> _carModelRepoMock = new();
        private readonly Mock<IDistributionCenterRepository> _centerRepoMock = new();
        private readonly Mock<ISaleRepository> _saleRepoMock = new();

        private readonly SaleCommandHandler _handler;

        public SaleCommandHandlerTests()
        {
            _handler = new SaleCommandHandler(
                _carModelRepoMock.Object,
                _centerRepoMock.Object,
                _saleRepoMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnSale_WhenAllDataIsValid()
        {
            // Arrange
            var carModel = new CarModel { CarModelName = "ModelX" };
            var centerId = Guid.NewGuid();
            var center = new DistributionCenter { Id = centerId, Name = "Center1" };
            var command = new RegisterSaleCommand("ModelX", "CHASSIS123", centerId);

            _carModelRepoMock.Setup(r => r.GetByModelAsync("ModelX")).ReturnsAsync(carModel);
            _centerRepoMock.Setup(r => r.GetByIdAsync(centerId)).ReturnsAsync(center);
            _saleRepoMock.Setup(r => r.SaveAsync(It.IsAny<Sale>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ModelX", result.CarModelName);
            Assert.Equal(centerId, result.DistributionCenterId);
            Assert.Equal(carModel, result.CarModel);
            Assert.Equal(center, result.DistributionCenter);
            _saleRepoMock.Verify(r => r.SaveAsync(It.Is<Sale>(s => s.CarModelName == "ModelX" && s.DistributionCenterId == centerId)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCarModelNotFound()
        {
            // Arrange
            var centerId = Guid.NewGuid();
            var command = new RegisterSaleCommand("UnknownModel", "CHASSIS123", centerId);

            _carModelRepoMock.Setup(r => r.GetByModelAsync("UnknownModel")).ReturnsAsync((CarModel?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command));
            Assert.Equal("Car Model not found", ex.Message);
            _centerRepoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _saleRepoMock.Verify(r => r.SaveAsync(It.IsAny<Sale>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCenterNotFound()
        {
            // Arrange
            var carModel = new CarModel { CarModelName = "ModelY" };
            var centerId = Guid.NewGuid();
            var command = new RegisterSaleCommand("ModelY", "CHASSIS456", centerId);

            _carModelRepoMock.Setup(r => r.GetByModelAsync("ModelY")).ReturnsAsync(carModel);
            _centerRepoMock.Setup(r => r.GetByIdAsync(centerId)).ReturnsAsync((DistributionCenter?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command));
            Assert.Equal("Center not found", ex.Message);
            _saleRepoMock.Verify(r => r.SaveAsync(It.IsAny<Sale>()), Times.Never);
        }
    }
}
