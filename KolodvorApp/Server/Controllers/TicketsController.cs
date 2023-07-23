using KolodvorApp.Domain.Services;
using KolodvorApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KolodvorApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _service;

    public TicketsController(ITicketService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> BuyTicket([FromBody] TicketPurchaseDto purchaseInfo)
    {
        try
        {
            await _service.BuyTicket(purchaseInfo);
            return Ok();
        }
        catch (Exception)
        {
            return Conflict("There's an error on the server.");
        }
    }
}