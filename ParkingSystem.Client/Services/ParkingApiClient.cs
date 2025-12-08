using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ParkingSystem.Shared.DTOs;

namespace ParkingSystem.Client.Services;

public class ParkingApiClient
{
    private readonly HttpClient _httpClient;

    public ParkingApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FloorDto>> GetFloorsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<FloorDto>>("api/parking/floors") ?? new List<FloorDto>();
    }

    public async Task<List<VehicleDto>> GetWaitingVehiclesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<VehicleDto>>("api/parking/waiting-vehicles") ?? new List<VehicleDto>();
    }

    public async Task CreateVehicleAsync(CreateVehicleDto dto)
    {
        await _httpClient.PostAsJsonAsync("api/parking/vehicles", dto);
    }

    public async Task ParkVehicleAsync(Guid vehicleId, Guid spotId)
    {
        await _httpClient.PostAsync($"api/parking/park?vehicleId={vehicleId}&spotId={spotId}", null);
    }

    public async Task UnparkVehicleAsync(Guid spotId)
    {
        await _httpClient.PostAsync($"api/parking/unpark?spotId={spotId}", null);
    }

    public async Task DeleteVehicleAsync(Guid vehicleId)
    {
        await _httpClient.DeleteAsync($"api/parking/vehicles/{vehicleId}");
    }
}
