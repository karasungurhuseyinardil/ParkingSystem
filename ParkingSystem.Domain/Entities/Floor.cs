using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem.Domain.Entities;

public class Floor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int FloorNumber { get; set; }
    public int Capacity { get; set; } = 20;
    
    public ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();

    public int OccupiedCount => ParkingSpots.Count(p => p.IsOccupied);
}
