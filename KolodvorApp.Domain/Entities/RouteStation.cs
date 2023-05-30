namespace KolodvorApp.Domain.Entities;

public class RouteStation : BaseEntity
{
    public DateTime? Arrival { get; set; }

    public TimeOnly? ArrivalTime
    {
        get => Arrival.HasValue ? TimeOnly.FromDateTime(Arrival.Value) : null;
        set => Arrival = value.HasValue ? new DateOnly(2020, 1, 1).ToDateTime(value.Value) : null;
    }

    public DateTime? Departure { get; set; }

    public TimeOnly? DepartureTime
    {
        get => Departure.HasValue ? TimeOnly.FromDateTime(Departure.Value) : null;
        set => Departure = value.HasValue ? new DateOnly(2020, 1, 1).ToDateTime(value.Value) : null;
    }

    public Guid TrainId { get; set; }

    public Guid StationId { get; set; }

    public Station Station { get; set; } = null!;

    public Guid RouteId { get; set; }

    public Route Route { get; set; } = null!;
}