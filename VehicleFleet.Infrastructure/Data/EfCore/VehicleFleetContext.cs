using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class VehicleFleetContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public VehicleFleetContext(DbContextOptions<VehicleFleetContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(_loggerFactory)
            //    .EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasMany(b => b.Vehicles).WithOne(p => p.Brand)
     .HasForeignKey(p => p.BrandId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Model>().HasMany(b => b.Vehicles).WithOne(p => p.Model)
    .HasForeignKey(p => p.ModelId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Brand>().HasMany(b => b.Models).WithOne(p => p.Brand)
    .HasForeignKey(p => p.BrandId)
    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}