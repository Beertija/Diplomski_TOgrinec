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

    public async Task<HttpResponseMessage> PromoteUser(Guid userId)
    {
        return await _http.PatchAsJsonAsync($"{RequestUri}/promote", userId);
    }

    public async Task<HttpResponseMessage> DemoteUser(Guid userId)
    {
        return await _http.PatchAsJsonAsync($"{RequestUri}/demote", userId);
    }

    public async Task<HttpResponseMessage> Register(RegisterUserDto user)
    {
        return await _http.PostAsJsonAsync($"{RequestUri}/register", user);
    }

    public async Task<HttpResponseMessage> Login(LoginUserDto user)
    {
        return await _http.PostAsJsonAsync($"{RequestUri}/login", user);
    }
}