using KolodvorApp.Shared.DTOs;
using System.Net.Http.Json;

namespace KolodvorApp.Client.HttpServices;

public class TrainMainenanceService : ITrainMaintenanceService
{
    private const string RequestUri = "/trainmaintenances";
    private readonly HttpClient _httpClient;

    public TrainMainenanceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> CreateOrUpdateTrainMaintenance(TrainMaintenanceDto trainMaintenance)
    {
        return await _httpClient.PostAsJsonAsync(RequestUri, trainMaintenance);
    }

    public async Task<HttpResponseMessage> DeleteTrainMaintenance(Guid trainMaintenanceId)
    {
        return await _httpClient.DeleteAsync($"{RequestUri}/{trainMaintenanceId}");
    }
}