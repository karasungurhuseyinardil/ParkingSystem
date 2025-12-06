using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingSystem.Domain.Entities;

namespace ParkingSystem.Application.Interfaces;

public interface IParkingRepository
{
    Task<List<Floor>> GetFloorsWithSpotsAsync();
    Task<List<Vehicle>> GetWaitingVehiclesAsync();
    Task<Vehicle?> GetVehicleByIdAsync(Guid id);
    Task<ParkingSpot?> GetSpotByIdAsync(Guid id);
    Task AddVehicleAsync(Vehicle vehicle);
    Task RemoveVehicleAsync(Vehicle vehicle);
    Task SaveChangesAsync();
}
