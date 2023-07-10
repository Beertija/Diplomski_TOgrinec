using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoutesController : ControllerBase
{
    private readonly IRouteService _service;
    private readonly IRouteCalculatorService _routeCalculatorService;

    public RoutesController(IRouteService service, IRouteCalculatorService routeCalculatorService)
    {
        _service = service;
        _routeCalculatorService = routeCalculatorService;
    }

    [HttpGet]
    public ActionResult<List<RouteDto>> GetAll()
    {
        var result = _service.GetAll();

        return Ok(result);
    }

    [HttpGet("route-stations")]
    public ActionResult<List<RouteStationDto>> GetAllRouteStations()
    {
        var result = _service.GetAllRouteStations();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RouteDto>> CreateOrUpdateAsync([FromBody] RouteDto routeDto)
    {
        try
        {
            var result = await _service.CreateOrUpdateAsync(routeDto);
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

    [HttpPost("search")]
    public ActionResult<List<MergedRoutesDto>> SearchRoutes([FromBody] RouteSearchDto searchInfo)
    {
        var result = _routeCalculatorService.FindTravelPaths(searchInfo);
        return Ok(result);
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
            return BadRequest($"Route {id} does not exist.");
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }
}