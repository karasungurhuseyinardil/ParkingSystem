using System;
using System.Collections.Generic;

namespace ParkingSystem.Shared.DTOs;

public class FloorDto
{
    public Guid Id { get; set; }
    public int FloorNumber { get; set; }
    public int Capacity { get; set; }
    public int OccupiedCount { get; set; }
    public List<ParkingSpotDto> ParkingSpots { get; set; } = new();
}
