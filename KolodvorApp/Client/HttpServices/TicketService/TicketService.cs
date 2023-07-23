using KolodvorApp.Shared.DTOs;
using System.Net.Http.Json;

namespace KolodvorApp.Client.HttpServices;

public class TicketService : ITicketService
{
    private const string RequestUri = "/tickets";
    private readonly HttpClient _http;

    public TicketService(HttpClient http)
    {
        _http = http;
    }

    public async Task<HttpResponseMessage> BuyTicket(TicketPurchaseDto purchaseInfo)
    {
        return await _http.PostAsJsonAsync(RequestUri, purchaseInfo);
    }
}