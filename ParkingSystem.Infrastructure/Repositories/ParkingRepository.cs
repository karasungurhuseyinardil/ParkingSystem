using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Domain.Entities;
using ParkingSystem.Infrastructure.Persistence;

namespace ParkingSystem.Infrastructure.Repositories;

public class ParkingRepository : IParkingRepository
{
    private readonly ParkingDbContext _context;

    public ParkingRepository(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Floor>> GetFloorsWithSpotsAsync()
    {
        return await _context.Floors
            .Include(f => f.ParkingSpots)
            .ThenInclude(p => p.Vehicle)
            .OrderBy(f => f.FloorNumber)
            .ToListAsync();
    }

    public async Task<List<Vehicle>> GetWaitingVehiclesAsync()
    {
        return await _context.Vehicles
            .Where(v => v.ParkingSpotId == null)
            .OrderBy(v => v.EntryTime)
            .ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(Guid id)
    {
        return await _context.Vehicles.FindAsync(id);
    }

    public async Task<ParkingSpot?> GetSpotByIdAsync(Guid id)
    {
        return await _context.ParkingSpots
            .Include(p => p.Vehicle)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
    }

    public async Task RemoveVehicleAsync(Vehicle vehicle)
    {
        _context.Vehicles.Remove(vehicle);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
