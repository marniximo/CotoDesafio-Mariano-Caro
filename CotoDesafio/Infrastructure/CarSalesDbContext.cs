using CotoDesafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                new DistributionCenter { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Centro Sur" },
                new DistributionCenter { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Centro Este" },
                new DistributionCenter { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Centro Oeste" }
            );

            // Seed de autos
            modelBuilder.Entity<CarModel>(builder =>
            {
                builder.HasData(
                    new CarModel { CarModelName = "Sedan", Price = 8000m, Tax = 0m },
                    new CarModel { CarModelName = "SUV", Price = 9500m, Tax = 0m },
                    new CarModel { CarModelName = "OffRoad", Price = 12500m, Tax = 0m },
                    new CarModel { CarModelName = "Sport", Price = 18200m, Tax = 7m }
                );
            });

            // Seed de ventas
            modelBuilder.Entity<Sale>().HasData(
                new Sale
                {
                    CarChassisNumber = "1234ejemplo",
                    CarModelName = "Sedan",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "1235ejemplo",
                    CarModelName = "Sedan",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "1236ejemplo",
                    CarModelName = "Sport",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "1237ejemplo",
                    CarModelName = "OffRoad",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "1238ejemplo",
                    CarModelName = "SUV",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "1239ejemplo",
                    CarModelName = "Sedan",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "1211ejemplo",
                    CarModelName = "Sport",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "11114ejemplo",
                    CarModelName = "OffRoad",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                },
                new Sale
                {
                    CarChassisNumber = "1223334ejemplo",
                    CarModelName = "SUV",
                    Date = DateTime.Parse("205-08-24"),
                    DistributionCenterId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CarModel = null, // Evito el error de referencia circular en el seed data
                    DistributionCenter = null,
                }
            );
        }
    }
}
