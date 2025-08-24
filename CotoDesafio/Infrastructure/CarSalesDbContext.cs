using CotoDesafio.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CotoDesafio.Infrastructure
{
    public class CarSalesDbContext : DbContext
    {
        public DbSet<Sale> Sales { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<DistributionCenter> DistributionCenters { get; set; }


        public CarSalesDbContext(DbContextOptions<CarSalesDbContext> options) : base(options) { }

        //En este metodo se arman las relaciones entre entidades, y se seedean los datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sale
            // Como no hay que mantener un stock de autos ni su existencia en ningun centro de venta,
            // no se modela la entidad Car. Basta con CarModel en Sale en este caso.
            modelBuilder.Entity<Sale>(builder =>
            {
                builder.HasOne(s => s.CarModel)
                       .WithMany(c => c.Sales)
                       .HasForeignKey(s => s.CarModelName) // FK en Sale
                       .IsRequired(); // La venta SIEMPRE tiene un tipo de auto

                builder.HasOne(s => s.DistributionCenter)
                       .WithMany(d => d.Sales)
                       .HasForeignKey(s => s.DistributionCenterId) // FK en Sale
                       .IsRequired(); // La venta SIEMPRE tiene un centro de distribucion
            });

            // Seed de centros
            modelBuilder.Entity<DistributionCenter>().HasData(
                new DistributionCenter { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Centro Norte" },
                new DistributionCenter { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Centro Sur" }
            );

            // Seed de autos
            modelBuilder.Entity<CarModel>().HasData(
                new CarModel { CarModelName = "Sedan", Price = new Money(8000, "USD"), Tax = 0m },
                new CarModel { CarModelName = "SUV", Price = new Money(9500, "USD"), Tax = 0m },
                new CarModel { CarModelName = "OffRoad", Price = new Money(12500, "USD"), Tax = 0m },
                new CarModel { CarModelName = "Sport", Price = new Money(18200, "USD"), Tax = 7m } // 7% de impuesto
            );

            // Seed de ventas
            modelBuilder.Entity<Sale>().HasData(
                new Sale
                {
                    CarChassisNumber = "1234ejemplo",
                    CarModelName = "Sedan",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Sale
                {
                    CarChassisNumber = "1235ejemplo",
                    CarModelName = "Sedan",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Sale
                {
                    CarChassisNumber = "1236ejemplo",
                    CarModelName = "Sport",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Sale
                {
                    CarChassisNumber = "1237ejemplo",
                    CarModelName = "OffRoad",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Sale
                {
                    CarChassisNumber = "1238ejemplo",
                    CarModelName = "SUV",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Sale
                {
                    CarChassisNumber = "1239ejemplo",
                    CarModelName = "Sedan",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Sale
                {
                    CarChassisNumber = "1211ejemplo",
                    CarModelName = "Sport",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Sale
                {
                    CarChassisNumber = "11114ejemplo",
                    CarModelName = "OffRoad",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Sale
                {
                    CarChassisNumber = "1223334ejemplo",
                    CarModelName = "SUV",
                    Date = DateTime.Now,
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                }
            );
        }
    }
}
