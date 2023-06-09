using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class StationsController : ControllerBase
{
    private readonly IStationService _service;

    public StationsController(IStationService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<StationDto>> GetAll()
    {
        var result = _service.GetAll();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<StationDto>>> GetAsync(Guid id)
    {
        try
        {
            var result = await _service.GetAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return BadRequest($"Station {id} does not exist.");
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }
}