using System;

namespace ParkingSystem.Shared.DTOs;

public class ParkingSpotDto
{
    public Guid Id { get; set; }
    public int SpotNumber { get; set; }
    public bool IsOccupied { get; set; }
    public Guid FloorId { get; set; }
    public VehicleDto? Vehicle { get; set; }
}
