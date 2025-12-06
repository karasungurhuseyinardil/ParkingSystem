using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingSystem.Shared.DTOs;

namespace ParkingSystem.Application.Interfaces;

public interface IParkingService
{
    Task<List<FloorDto>> GetFloorsAsync();
    Task<List<VehicleDto>> GetWaitingVehiclesAsync();
    Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto dto);
    Task<bool> ParkVehicleAsync(Guid vehicleId, Guid spotId);
    Task<bool> UnparkVehicleAsync(Guid spotId);
    Task<bool> DeleteVehicleAsync(Guid vehicleId);
}
