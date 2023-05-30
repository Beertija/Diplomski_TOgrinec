namespace KolodvorApp.Domain.Entities;

public class Route : BaseEntity
{
    public bool IsDaily { get; set; }

    public decimal Cost { get; set; }

    public Guid FirstStation { get; set; }

    public Guid LastStation { get; set; }

    public virtual ICollection<RouteStation> RouteStations { get; set; } = new List<RouteStation>();

    public virtual ICollection<RouteTrain> RouteTrains { get; set; } = new List<RouteTrain>();
}