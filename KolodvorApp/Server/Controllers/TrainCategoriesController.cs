using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainCategoriesController : ControllerBase
{
    private readonly ITrainCategoryService _service;

    public TrainCategoriesController(ITrainCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<TrainCategoryDto>> GetAll()
    {
        var result = _service.GetAll();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainCategoryDto>> GetAsync(Guid id)
    {
        try
        {
            var result = await _service.GetAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return BadRequest($"Train category {id} does not exist.");
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }
}