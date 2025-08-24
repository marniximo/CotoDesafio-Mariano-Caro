using CotoDesafio.Domain;
using CotoDesafio.Infrastructure;
using CotoDesafio.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace CotoDesafio.UnitTests.Infrastructure
{
    public class CarModelReadRepositoryTests
    {
        private CarSalesDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CarSalesDbContext>()
                .UseInMemoryDatabase(databaseName: "CarModelReadRepositoryTestsDb")
                .Options;
            return new CarSalesDbContext(options);
        }

        [Fact]
        public async Task GetByModelAsync_Returns_CarModel_When_Exists()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var carModel = new CarModel { CarModelName = "ModelA" };
            context.CarModels.Add(carModel);
            await context.SaveChangesAsync();

            var repo = new CarModelReadRepository(context);

            // Act
            var result = await repo.GetByModelAsync("ModelA");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ModelA", result.CarModelName);
        }

        [Fact]
        public async Task GetByModelAsync_Returns_Null_When_Not_Exists()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repo = new CarModelReadRepository(context);

            // Act
            var result = await repo.GetByModelAsync("NonExistentModel");

            // Assert
            Assert.Null(result);
        }
    }
}