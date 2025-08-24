using CotoDesafio.Domain;
using CotoDesafio.Infrastructure;
using CotoDesafio.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CotoDesafio.UnitTests.Infrastructure
{
    public class SalesReadRepositoryTests
    {
        private static CarSalesDbContext CreateInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CarSalesDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new CarSalesDbContext(options);
        }

        [Fact]
        public async Task GetTotalSalesByCenterAsync_ReturnsSalesForGivenCenter()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateInMemoryDbContext(dbName);

            var centerId = Guid.NewGuid();
            var otherCenterId = Guid.NewGuid();

            // Create and add a shared CarModel
            var carModel = new CarModel { CarModelName = "Sedan", Price = 10000m, Tax = 7.5m };
            context.CarModels.Add(carModel);

            // Create and add a shared DistributionCenter for centerId
            var distributionCenter = new DistributionCenter { Id = centerId, Name = "Center 1" };
            var otherDistributionCenter = new DistributionCenter { Id = otherCenterId, Name = "Center 2" };
            context.DistributionCenters.AddRange(distributionCenter, otherDistributionCenter);

            await context.SaveChangesAsync();

            var sales = new List<Sale>
            {
                new Sale
                {
                    CarChassisNumber = Guid.NewGuid().ToString(),
                    DistributionCenterId = centerId,
                    DistributionCenter = distributionCenter,
                    CarModelName = carModel.CarModelName,
                    CarModel = carModel,
                    Date = DateTime.UtcNow
                },
                new Sale
                {
                    CarChassisNumber = Guid.NewGuid().ToString(),
                    DistributionCenterId = centerId,
                    DistributionCenter = distributionCenter,
                    CarModelName = carModel.CarModelName,
                    CarModel = carModel,
                    Date = DateTime.UtcNow
                },
                new Sale
                {
                    CarChassisNumber = Guid.NewGuid().ToString(),
                    DistributionCenterId = otherCenterId,
                    DistributionCenter = otherDistributionCenter,
                    CarModelName = carModel.CarModelName,
                    CarModel = carModel,
                    Date = DateTime.UtcNow
                }
            };

            context.Sales.AddRange(sales);
            await context.SaveChangesAsync();

            var repository = new SalesReadRepository(context);

            // Act
            var result = await repository.GetTotalSalesByCenterAsync(centerId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, s => Assert.Equal(centerId, s.DistributionCenterId));
        }

        [Fact]
        public async Task GetTotalSalesByCenterAsync_ReturnsEmptyList_WhenNoSalesForCenter()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateInMemoryDbContext(dbName);

            var centerId = Guid.NewGuid();

            // Create and add a shared CarModel
            var carModel = new CarModel { CarModelName = "SUV", Price = 20000m, Tax = 10m };
            context.CarModels.Add(carModel);

            // Create and add a DistributionCenter (not used in sale)
            var distributionCenter = new DistributionCenter { Id = centerId, Name = "Center 1" };
            context.DistributionCenters.Add(distributionCenter);

            await context.SaveChangesAsync();

            // Sale for a different center
            context.Sales.Add(new Sale
            {
                CarChassisNumber = Guid.NewGuid().ToString(),
                DistributionCenterId = Guid.NewGuid(),
                CarModelName = carModel.CarModelName,
                CarModel = carModel,
                Date = DateTime.UtcNow
            });
            await context.SaveChangesAsync();

            var repository = new SalesReadRepository(context);

            // Act
            var result = await repository.GetTotalSalesByCenterAsync(centerId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTotalSalesCount_ReturnsTotalSalesCount()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateInMemoryDbContext(dbName);

            // Create and add a shared CarModel
            var carModel = new CarModel { CarModelName = "Hatchback", Price = 15000m, Tax = 8m };
            context.CarModels.Add(carModel);

            // Create and add DistributionCenters
            var center1 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center 1" };
            var center2 = new DistributionCenter { Id = Guid.NewGuid(), Name = "Center 2" };
            context.DistributionCenters.AddRange(center1, center2);

            await context.SaveChangesAsync();

            context.Sales.AddRange(
                new Sale
                {
                    CarChassisNumber = Guid.NewGuid().ToString(),
                    DistributionCenterId = center1.Id,
                    DistributionCenter = center1,
                    CarModelName = carModel.CarModelName,
                    CarModel = carModel,
                    Date = DateTime.UtcNow
                },
                new Sale
                {
                    CarChassisNumber = Guid.NewGuid().ToString(),
                    DistributionCenterId = center2.Id,
                    DistributionCenter = center2,
                    CarModelName = carModel.CarModelName,
                    CarModel = carModel,
                    Date = DateTime.UtcNow
                }
            );
            await context.SaveChangesAsync();

            var repository = new SalesReadRepository(context);

            // Act
            var count = await repository.GetTotalSalesCount();

            // Assert
            Assert.Equal(2, count);
        }
    }
}