using KolodvorApp.Shared.DTOs;
using System.Net.Http.Json;

namespace KolodvorApp.Client.HttpServices;

public class TrainService : ITrainService
{
    private const string RequestUri = "/trains";
    private readonly HttpClient _http;

    public TrainService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TrainDto>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<TrainDto>>(RequestUri);
    }

    public async Task<TrainDto> GetTrainByIdWithMaintenances(Guid trainId)
    {
        return await _http.GetFromJsonAsync<TrainDto>($"{RequestUri}/{trainId}/{true}");
    }
}