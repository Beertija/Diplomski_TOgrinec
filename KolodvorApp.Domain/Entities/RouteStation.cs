namespace KolodvorApp.Domain.Entities;

public class RouteStation : BaseEntity
{
    public DateTime Arrival { get; set; }

    public TimeOnly ArrivalTime
    {
        get => TimeOnly.FromDateTime(Arrival);
        set => Arrival = new DateOnly(2020, 1, 1).ToDateTime(value);
    }

    public DateTime Departure { get; set; }

    public TimeOnly DepartureTime
    {
        get => TimeOnly.FromDateTime(Departure);
        set => Departure = new DateOnly(2020, 1, 1).ToDateTime(value);
    }

    public Guid StartStationId { get; set; }

    public virtual Station StartStation { get; set; } = null!;

    public Guid EndStationId { get; set; }

    public virtual Station EndStation { get; set; } = null!;

    public Guid RouteId { get; set; }

    public Route Route { get; set; } = null!;

    public decimal Cost { get; set; }

    public int Order { get; set; }
}