using KolodvorApp.Shared.DTOs;
using System.Net.Http.Json;

namespace KolodvorApp.Client.HttpServices;

public class TrainMainenanceService : ITrainMaintenanceService
{
    private const string RequestUri = "/trainmaintenances";
    private readonly HttpClient _http;

    public TrainMainenanceService(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<HttpResponseMessage> CreateOrUpdateTrainMaintenance(TrainMaintenanceDto trainMaintenance)
    {
        return await _http.PostAsJsonAsync(RequestUri, trainMaintenance);
    }

    public async Task<HttpResponseMessage> DeleteTrainMaintenance(Guid trainMaintenanceId)
    {
        return await _http.DeleteAsync($"{RequestUri}/{trainMaintenanceId}");
    }
}