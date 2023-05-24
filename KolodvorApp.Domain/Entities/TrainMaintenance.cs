namespace KolodvorApp.Domain.Entities;

public class TrainMaintenance : BaseEntity
{
    public string Maintenance { get; set; } = null!;

    public decimal Cost { get; set; }

    public Guid TrainId { get; set; }

    public virtual Train Train { get; set; }
}