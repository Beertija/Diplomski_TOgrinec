using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainsController : ControllerBase
{
    private readonly ITrainService _service;
    private readonly ITrainCategoryService _trainCategoryService;

    public TrainsController(ITrainService service, ITrainCategoryService trainCategoryService)
    {
        _service = service;
        _trainCategoryService = trainCategoryService;
    }

    [HttpGet]
    public ActionResult<List<TrainDto>> GetAll()
    {
        var result = _service.GetAll();

        return Ok(result);
    }

    [HttpGet("categories")]
    public ActionResult<List<TrainCategoryDto>> GetAllCategories()
    {
        var result = _trainCategoryService.GetAll();

        return Ok(result);
    }

    [HttpGet("{id}/{includeMaintaining}")]
    public async Task<ActionResult<TrainDto>> GetAsync(Guid id, bool includeMaintaining)
    {
        try
        {
            var result = await _service.GetAsync(id, includeMaintaining);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return BadRequest($"Train {id} does not exist.");
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TrainDto>> CreateOrUpdateAsync([FromBody] TrainDto trainDto)
    {
        try
        {
            var result = await _service.CreateOrUpdateAsync(trainDto);
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
            return BadRequest($"Train {id} does not exist.");
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }
}