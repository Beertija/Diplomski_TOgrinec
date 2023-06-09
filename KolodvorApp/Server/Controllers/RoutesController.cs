using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoutesController : ControllerBase
{
    private readonly IRouteService _service;

    public RoutesController(IRouteService service)
    {
        _service = service;
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
}