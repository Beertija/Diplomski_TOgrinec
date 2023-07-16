using KolodvorApp.Shared.DTOs;
using System.Net.Http.Json;

namespace KolodvorApp.Client.HttpServices;

public class UserService : IUserService
{
    private const string RequestUri = "/users";
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<UserDto>> GetAllEmployees()
    {
        return await _http.GetFromJsonAsync<List<UserDto>>($"{RequestUri}/employees");
    }

    public async Task<List<UserDto>> GetAllRegularUsers()
    {
        return await _http.GetFromJsonAsync<List<UserDto>>($"{RequestUri}/regular");
    }
}