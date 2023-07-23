using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface IUserService
{
    List<UserDto> GetAllEmployees();

    List<UserDto> GetAllRegularUsers();

    Task PromoteUser(Guid userId);

    Task DemoteUser(Guid userId);

    Task Register(RegisterUserDto userDto);

    LoggedUserDto Login(LoginUserDto userDto);
}