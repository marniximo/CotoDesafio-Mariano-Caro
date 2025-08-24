using CotoDesafio.Domain;
using CotoDesafio.Infrastructure;
using CotoDesafio.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CotoDesafio.UnitTests.Infrastructure
{
    public class SalesRepositoryTests
    {
        private static CarSalesDbContext CreateInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CarSalesDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new CarSalesDbContext(options);
        }

        [Fact]
        public async Task SaveAsync_AddsSaleToContext()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateInMemoryDbContext(dbName);

            // Add required CarModel and DistributionCenter
            var carModel = new CarModel { CarModelName = "TestModel", Price = 10000m, Tax = 5m };
            var distributionCenter = new DistributionCenter { Id = Guid.NewGuid(), Name = "Test Center" };
            context.CarModels.Add(carModel);
            context.DistributionCenters.Add(distributionCenter);
            await context.SaveChangesAsync();

            var sale = new Sale
            {
                CarChassisNumber = Guid.NewGuid().ToString(),
                CarModelName = carModel.CarModelName,
                CarModel = carModel,
                DistributionCenterId = distributionCenter.Id,
                DistributionCenter = distributionCenter,
                Date = DateTime.UtcNow
            };

            var repository = new SalesRepository(context);

            // Act
            await repository.SaveAsync(sale);
            await context.SaveChangesAsync();

            // Assert
            var savedSale = await context.Sales.FirstOrDefaultAsync(s => s.CarChassisNumber == sale.CarChassisNumber);
            Assert.NotNull(savedSale);
            Assert.Equal(carModel.CarModelName, savedSale.CarModelName);
            Assert.Equal(distributionCenter.Id, savedSale.DistributionCenterId);
        }
    }
}