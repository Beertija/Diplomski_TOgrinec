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
}