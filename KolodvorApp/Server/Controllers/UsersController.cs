using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("employees")]
    public ActionResult<List<UserDto>> GetAllEmployees()
    {
        var result = _service.GetAllEmployees();

        return Ok(result);
    }

    [HttpGet("regular")]
    public ActionResult<List<UserDto>> GetAllRegularUsers()
    {
        var result = _service.GetAllRegularUsers();

        return Ok(result);
    }

    [HttpPatch("promote")]
    public async Task<IActionResult> PromoteUserAsync([FromBody] Guid userId)
    {
        try
        {
            await _service.PromoteUser(userId);
            return Ok();
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

    [HttpPatch("demote")]
    public async Task<IActionResult> DemoteUserAsync([FromBody] Guid userId)
    {
        try
        {
            await _service.DemoteUser(userId);
            return Ok();
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
    {
        try
        {
            await _service.Register(user);
            return Ok();
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }

    [HttpPost("login")]
    public ActionResult<UserDto> Login([FromBody] LoginUserDto userDto)
    {
        try
        {
            var user = _service.Login(userDto);
            return Ok(user);
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
}