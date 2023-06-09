namespace KolodvorApp.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

public enum Role
{
    User,
    Worker,
    Admin
}