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
    public class DistributionCenterRepositoryTests
    {
        private static CarSalesDbContext CreateInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CarSalesDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new CarSalesDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllDistributionCenters()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateInMemoryDbContext(dbName);
            var centers = new List<DistributionCenter>
            {
                new DistributionCenter { Id = Guid.NewGuid(), Name = "Center 1" },
                new DistributionCenter { Id = Guid.NewGuid(), Name = "Center 2" }
            };
            context.DistributionCenters.AddRange(centers);
            await context.SaveChangesAsync();

            var repository = new DistributionCenterRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Center 1");
            Assert.Contains(result, c => c.Name == "Center 2");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectDistributionCenter_WhenExists()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateInMemoryDbContext(dbName);
            var center = new DistributionCenter { Id = Guid.NewGuid(), Name = "Main Center" };
            context.DistributionCenters.Add(center);
            await context.SaveChangesAsync();

            var repository = new DistributionCenterRepository(context);

            // Act
            var result = await repository.GetByIdAsync(center.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(center.Id, result!.Id);
            Assert.Equal("Main Center", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotExists()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateInMemoryDbContext(dbName);
            var repository = new DistributionCenterRepository(context);

            // Act
            var result = await repository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}