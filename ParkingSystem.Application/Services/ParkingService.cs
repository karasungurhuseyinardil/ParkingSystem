using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Domain.Entities;
using ParkingSystem.Domain.Enums;
using ParkingSystem.Shared.DTOs;

namespace ParkingSystem.Application.Services;

public class ParkingService : IParkingService
{
    private readonly IParkingRepository _repository;

    public ParkingService(IParkingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<FloorDto>> GetFloorsAsync()
    {
        var floors = await _repository.GetFloorsWithSpotsAsync();

        return floors.Select(f => new FloorDto
        {
            Id = f.Id,
            FloorNumber = f.FloorNumber,
            Capacity = f.Capacity,
            OccupiedCount = f.OccupiedCount,
            ParkingSpots = f.ParkingSpots.Select(p => new ParkingSpotDto
            {
                Id = p.Id,
                SpotNumber = p.SpotNumber,
                IsOccupied = p.IsOccupied,
                FloorId = p.FloorId,
                Vehicle = p.Vehicle != null ? new VehicleDto
                {
                    Id = p.Vehicle.Id,
                    LicensePlate = p.Vehicle.LicensePlate,
                    EntryTime = p.Vehicle.EntryTime,
                    Type = p.Vehicle.Type.ToString(),
                    ParkingSpotId = p.Vehicle.ParkingSpotId
                } : null
            }).OrderBy(p => p.SpotNumber).ToList()
        }).ToList();
    }

    public async Task<List<VehicleDto>> GetWaitingVehiclesAsync()
    {
        var vehicles = await _repository.GetWaitingVehiclesAsync();

        return vehicles.Select(v => new VehicleDto
        {
            Id = v.Id,
            LicensePlate = v.LicensePlate,
            EntryTime = v.EntryTime,
            Type = v.Type.ToString(),
            ParkingSpotId = v.ParkingSpotId
        }).ToList();
    }

    public async Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto dto)
    {
        if (!Enum.TryParse<VehicleType>(dto.Type, out var type))
        {
            type = VehicleType.Car;
        }

        var vehicle = new Vehicle
        {
            LicensePlate = dto.LicensePlate,
            Type = type,
            EntryTime = DateTime.Now
        };

        await _repository.AddVehicleAsync(vehicle);
        await _repository.SaveChangesAsync();

        return new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            EntryTime = vehicle.EntryTime,
            Type = vehicle.Type.ToString(),
            ParkingSpotId = vehicle.ParkingSpotId
        };
    }

    public async Task<bool> ParkVehicleAsync(Guid vehicleId, Guid spotId)
    {
        var vehicle = await _repository.GetVehicleByIdAsync(vehicleId);
        if (vehicle == null) return false;

        var spot = await _repository.GetSpotByIdAsync(spotId);
        if (spot == null) return false;

        if (spot.IsOccupied) return false;

        // If vehicle was already parked elsewhere
        if (vehicle.ParkingSpotId != null)
        {
             var oldSpot = await _repository.GetSpotByIdAsync(vehicle.ParkingSpotId.Value);
             if (oldSpot != null)
             {
                 oldSpot.IsOccupied = false;
                 oldSpot.VehicleId = null;
             }
        }

        spot.IsOccupied = true;
        spot.VehicleId = vehicle.Id;
        vehicle.ParkingSpotId = spot.Id;

        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnparkVehicleAsync(Guid spotId)
    {
        var spot = await _repository.GetSpotByIdAsync(spotId);
        if (spot == null || !spot.IsOccupied || spot.Vehicle == null) return false;

        var vehicle = spot.Vehicle;
        
        spot.IsOccupied = false;
        spot.VehicleId = null;
        vehicle.ParkingSpotId = null; 
        
        await _repository.RemoveVehicleAsync(vehicle);

        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteVehicleAsync(Guid vehicleId)
    {
        var vehicle = await _repository.GetVehicleByIdAsync(vehicleId);
        if (vehicle == null) return false;

        if (vehicle.ParkingSpotId != null)
        {
            var spot = await _repository.GetSpotByIdAsync(vehicle.ParkingSpotId.Value);
            if (spot != null)
            {
                spot.IsOccupied = false;
                spot.VehicleId = null;
            }
        }

        await _repository.RemoveVehicleAsync(vehicle);
        await _repository.SaveChangesAsync();
        return true;
    }
}
