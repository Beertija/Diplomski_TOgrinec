namespace KolodvorApp.Domain.Entities;

public class Route : BaseEntity
{
    public bool IsDaily { get; set; }

    public Guid TrainId { get; set; }

    public Train Train { get; set; } = null!;

    public virtual ICollection<RouteStation> RouteStations { get; set; } = new List<RouteStation>();
}