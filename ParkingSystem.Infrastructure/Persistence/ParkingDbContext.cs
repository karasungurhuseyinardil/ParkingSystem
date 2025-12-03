using Microsoft.EntityFrameworkCore;
using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Infrastructure.Persistence;

public class ParkingDbContext : DbContext
{
    public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
    {
    }

    public DbSet<Floor> Floors { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.ParkingSpot)
            .WithOne(p => p.Vehicle)
            .HasForeignKey<Vehicle>(v => v.ParkingSpotId)
            .OnDelete(DeleteBehavior.SetNull); // If spot is deleted, vehicle is unparked? Or restrict?
            // Actually, if Vehicle is deleted, spot becomes free. 
            // If Spot is deleted... well spots are static.
            // Let's stick to simple relation.
            
        modelBuilder.Entity<ParkingSpot>()
            .HasOne(p => p.Floor)
            .WithMany(f => f.ParkingSpots)
            .HasForeignKey(p => p.FloorId);
    }
}
