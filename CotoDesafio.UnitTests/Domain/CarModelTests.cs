using CotoDesafio.Domain;
using Xunit;

namespace CotoDesafio.UnitTests.Domain
{
    public class CarModelTests
    {
        [Fact]
        public void GetFinalPrice_Returns_Price_With_Tax()
        {
            // Arrange
            var carModel = new CarModel
            {
                CarModelName = "Sport",
                Price = new Money(100_000, "USD"),
                Tax = 10m // 10%
            };

            // Act
            var finalPrice = carModel.GetFinalPrice();

            // Assert
            Assert.Equal(110_000, finalPrice.Amount);
            Assert.Equal("USD", finalPrice.Currency);
        }

        [Fact]
        public void GetFinalPrice_Returns_Original_Price_When_Tax_Is_Zero()
        {
            // Arrange
            var carModel = new CarModel
            {
                CarModelName = "Basic",
                Price = new Money(50_000, "USD"),
                Tax = 0m
            };

            // Act
            var finalPrice = carModel.GetFinalPrice();

            // Assert
            Assert.Equal(50_000, finalPrice.Amount);
            Assert.Equal("USD", finalPrice.Currency);
        }

        [Fact]
        public void GetFinalPrice_Handles_Negative_Tax()
        {
            // Arrange
            var carModel = new CarModel
            {
                CarModelName = "Discounted",
                Price = new Money(20_000, "USD"),
                Tax = -10m // -10% (should reduce price)
            };

            // Act
            var finalPrice = carModel.GetFinalPrice();

            // Assert
            Assert.Equal(18_000, finalPrice.Amount);
            Assert.Equal("USD", finalPrice.Currency);
        }

        [Fact]
        public void CarModel_Default_Properties_Are_Initialized()
        {
            // Arrange
            var carModel = new CarModel();

            // Assert
            Assert.Equal(string.Empty, carModel.CarModelName);
            Assert.Equal(0, carModel.Price.Amount);
            Assert.Equal("USD", carModel.Price.Currency);
            Assert.Equal(0, carModel.Tax);
            Assert.NotNull(carModel.Sales);
        }
    }
}