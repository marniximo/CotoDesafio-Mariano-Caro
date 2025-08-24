using System;
using CotoDesafio.Domain;
using Xunit;

namespace CotoDesafio.UnitTests.Domain
{
    public class SaleTests
    {
        [Fact]
        public void GetFinalSalePrice_Returns_CarModel_FinalPrice()
        {
            // Arrange
            var carModel = new CarModel
            {
                CarModelName = "TestModel",
                Price = new Money(100_000, "USD"),
                Tax = 15m
            };
            var sale = new Sale
            {
                CarChassisNumber = "CHASSIS001",
                CarModelName = "TestModel",
                CarModel = carModel,
                DistributionCenterId = Guid.NewGuid(),
                DistributionCenter = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center1" },
                Date = DateTime.UtcNow
            };

            // Act
            var finalPrice = sale.GetFinalSalePrice();

            // Assert
            Assert.Equal(115_000, finalPrice.Amount);
            Assert.Equal("USD", finalPrice.Currency);
        }

        [Fact]
        public void Sale_Default_Properties_Are_Initialized()
        {
            // Arrange
            var sale = new Sale();

            // Assert
            Assert.Equal(string.Empty, sale.CarChassisNumber);
            Assert.Equal(string.Empty, sale.CarModelName);
            Assert.NotNull(sale.CarModel);
            Assert.Equal(Guid.Empty, sale.DistributionCenterId);
            Assert.NotNull(sale.DistributionCenter);
            Assert.Equal(default(DateTime), sale.Date);
        }
    }
}