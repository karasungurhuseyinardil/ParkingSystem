using System;

namespace ParkingSystem.Domain.Entities;

public class ParkingSpot
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int SpotNumber { get; set; }
    public bool IsOccupied { get; set; }
    
    public Guid FloorId { get; set; }
    public Floor Floor { get; set; } = null!;
    
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
}
