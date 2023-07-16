using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface IUserService
{
    List<UserDto> GetAllEmployees();

    List<UserDto> GetAllRegularUsers();
}