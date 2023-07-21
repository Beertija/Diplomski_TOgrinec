namespace KolodvorApp.Shared.DTOs;

public class UserDto
{
    public Guid? Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public RoleEnum Role { get; set; }
}