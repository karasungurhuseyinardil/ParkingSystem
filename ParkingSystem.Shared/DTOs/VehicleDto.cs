using System;

namespace ParkingSystem.Shared.DTOs;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public DateTime EntryTime { get; set; }
    public string Type { get; set; } = string.Empty;
    public Guid? ParkingSpotId { get; set; }
}

public class CreateVehicleDto
{
    public string LicensePlate { get; set; } = string.Empty;
    public string Type { get; set; } = "Car";
}
