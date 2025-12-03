using System;
using System.Collections.Generic;
using System.Linq;
using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Initialize(ParkingDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Floors.Any())
        {
            return;   // DB has been seeded
        }

        var floors = new List<Floor>();
        for (int i = 1; i <= 1; i++)
        {
            var floor = new Floor
            {
                FloorNumber = i,
                Capacity = 20
            };
            
            for (int j = 1; j <= 20; j++)
            {
                floor.ParkingSpots.Add(new ParkingSpot
                {
                    SpotNumber = j,
                    IsOccupied = false
                });
            }
            floors.Add(floor);
        }

        context.Floors.AddRange(floors);
        context.SaveChanges();
    }
}
