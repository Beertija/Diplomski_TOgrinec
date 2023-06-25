namespace KolodvorApp.Domain.Entities;

public class Ticket : BaseEntity
{
    public decimal Cost { get; set; }

    public string StartStation { get; set; } = null!;

    public string EndStation { get; set; } = null!;

    public Guid UserId { get; set;}

    public virtual User User { get; set; } = null!;
}