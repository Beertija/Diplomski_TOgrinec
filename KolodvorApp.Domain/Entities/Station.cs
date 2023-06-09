namespace KolodvorApp.Domain.Entities;

public class Station : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<RouteStation> StartRouteStations { get; set; } = new List<RouteStation>();

    public virtual ICollection<RouteStation> EndRouteStations { get; set; } = new List<RouteStation>();
}