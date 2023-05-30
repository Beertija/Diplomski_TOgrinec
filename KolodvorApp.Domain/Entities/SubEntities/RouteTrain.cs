namespace KolodvorApp.Domain.Entities;

public class RouteTrain
{
    public Guid TrainId { get; set; }

    public Train Train { get; set; } = null!;

    public Guid RouteId { get; set; }

    public Route Route { get; set; } = null!;
}