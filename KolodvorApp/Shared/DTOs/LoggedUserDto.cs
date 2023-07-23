namespace KolodvorApp.Shared.DTOs;

public class LoggedUserDto
{
    public Guid? Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public RoleEnum Role { get; set; }

    public string JwtToken { get; set; } = null!;
}
