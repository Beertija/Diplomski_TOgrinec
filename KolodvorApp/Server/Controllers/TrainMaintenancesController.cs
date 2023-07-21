using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainMaintenancesController : ControllerBase
{
    private readonly ITrainMaintenanceService _service;

    public TrainMaintenancesController(ITrainMaintenanceService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<TrainMaintenanceDto>> CreateOrUpdateAsync([FromBody] TrainMaintenanceDto trainMaintenanceDto)
    {
        try
        {
            var result = await _service.CreateOrUpdateAsync(trainMaintenanceDto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return BadRequest($"Train maintenance {id} does not exist.");
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }
}