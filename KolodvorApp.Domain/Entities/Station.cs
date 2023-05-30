namespace KolodvorApp.Domain.Entities;

public class Station : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<RouteStation> RouteStations { get; set; } = new List<RouteStation>();
}