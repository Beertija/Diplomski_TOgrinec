using System.Text.Json.Serialization;

namespace KolodvorApp.Shared.DTOs;

public class RouteStationDto
{
    public Guid Id { get; set; }

    public decimal Cost { get; set; }

    public int Order { get; set; }

    [property: JsonConverter(typeof(TimeOnlyConverter))]
    public TimeOnly ArrivalTime { get; set; }

    [property: JsonConverter(typeof(TimeOnlyConverter))]
    public TimeOnly DepartureTime { get; set; }

    public Guid StartStationId { get; set; }

    public StationDto? StartStation { get; set; }

    public Guid EndStationId { get; set; }

    public StationDto? EndStation { get; set; }

    public Guid RouteId { get; set; }
}