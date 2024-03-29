﻿using KolodvorApp.Shared.DTOs;
using System.Net.Http.Json;

namespace KolodvorApp.Client.HttpServices;

public class RouteService : IRouteService
{
    private const string RequestUri = "/routes";
    private readonly HttpClient _http;

    public RouteService(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<List<RouteDto>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<RouteDto>>(RequestUri);
    }

    public async Task<HttpResponseMessage> CreateOrUpdateRoute(RouteDto route)
    {
        return await _http.PostAsJsonAsync(RequestUri, route);
    }

    public async Task<HttpResponseMessage> DeleteRoute(Guid routeId)
    {
        return await _http.DeleteAsync($"{RequestUri}/{routeId}");
    }

    public async Task<HttpResponseMessage> SearchRoutes(RouteSearchDto searchInfo)
    {
        return await _http.PostAsJsonAsync($"{RequestUri}/search", searchInfo);
    }
}