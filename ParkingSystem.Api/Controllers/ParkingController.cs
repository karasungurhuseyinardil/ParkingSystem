using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Shared.DTOs;

namespace ParkingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingController : ControllerBase
{
    private readonly IParkingService _parkingService;

    public ParkingController(IParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    [HttpGet("floors")]
    public async Task<ActionResult<List<FloorDto>>> GetFloors()
    {
        return Ok(await _parkingService.GetFloorsAsync());
    }

    [HttpGet("waiting-vehicles")]
    public async Task<ActionResult<List<VehicleDto>>> GetWaitingVehicles()
    {
        return Ok(await _parkingService.GetWaitingVehiclesAsync());
    }

    [HttpPost("vehicles")]
    public async Task<ActionResult<VehicleDto>> CreateVehicle([FromBody] CreateVehicleDto dto)
    {
        var vehicle = await _parkingService.CreateVehicleAsync(dto);
        return CreatedAtAction(nameof(GetWaitingVehicles), new { id = vehicle.Id }, vehicle);
    }

    [HttpPost("park")]
    public async Task<IActionResult> ParkVehicle([FromQuery] Guid vehicleId, [FromQuery] Guid spotId)
    {
        var result = await _parkingService.ParkVehicleAsync(vehicleId, spotId);
        if (!result) return BadRequest("Unable to park vehicle. Spot might be occupied or vehicle not found.");
        return Ok();
    }

    [HttpPost("unpark")]
    public async Task<IActionResult> UnparkVehicle([FromQuery] Guid spotId)
    {
        var result = await _parkingService.UnparkVehicleAsync(spotId);
        if (!result) return BadRequest("Unable to unpark vehicle.");
        return Ok();
    }

    [HttpDelete("vehicles/{id}")]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        var result = await _parkingService.DeleteVehicleAsync(id);
        if (!result) return NotFound();
        return Ok();
    }
}
