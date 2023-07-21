namespace KolodvorApp.Shared.DTOs;

public class LoginUserDto
{
    public string EmailOrUsername { get; set; } = null!;

    public string Password { get; set; } = null!;
}