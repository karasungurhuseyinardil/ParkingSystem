using System;
using ParkingSystem.Domain.Enums;

namespace ParkingSystem.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string LicensePlate { get; set; } = string.Empty;
    public DateTime EntryTime { get; set; } = DateTime.Now;
    public VehicleType Type { get; set; }
    
    // Nullable because a vehicle might be in the waiting list (not parked yet)
    public Guid? ParkingSpotId { get; set; }
    public ParkingSpot? ParkingSpot { get; set; }
}
