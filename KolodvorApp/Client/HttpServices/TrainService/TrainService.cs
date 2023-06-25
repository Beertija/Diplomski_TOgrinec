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

    public async Task<List<TrainCategoryDto>> GetAllCategories()
    {
        return await _http.GetFromJsonAsync<List<TrainCategoryDto>>($"{RequestUri}/categories");
    }

    public async Task<TrainDto> GetTrainByIdWithMaintenances(Guid trainId)
    {
        return await _http.GetFromJsonAsync<TrainDto>($"{RequestUri}/{trainId}/{true}");
    }

    public async Task<HttpResponseMessage> CreateOrUpdateTrain(TrainDto train)
    {
        return await _http.PostAsJsonAsync(RequestUri, train);
    }

    public async Task<HttpResponseMessage> DeleteTrain(Guid trainId)
    {
        return await _http.DeleteAsync($"{RequestUri}/{trainId}");
    }
}