using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface IUserService
{
    Task<List<UserDto>> GetAllEmployees();

    Task<List<UserDto>> GetAllRegularUsers();
}