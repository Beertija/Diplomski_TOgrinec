using KolodvorApp.Shared.DTOs;
using System.Net.Http.Json;

namespace KolodvorApp.Client.HttpServices;

public class StationService : IStationService
{
    private const string RequestUri = "/stations";
    private readonly HttpClient _http;

    public StationService(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<List<StationDto>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<StationDto>>(RequestUri);
    }
}