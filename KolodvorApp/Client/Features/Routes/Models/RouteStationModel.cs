using KolodvorApp.Shared.DTOs;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KolodvorApp.Client.Features.Routes.Models;

public class RouteStationModel
{
    public decimal Cost { get; set; }

    public int Order { get; set; }

    [property: JsonConverter(typeof(TimeOnlyConverter))]
    public TimeOnly? ArrivalTime { get; set; }

    public TimeSpan? StartTime
    {
        get { return ArrivalTime?.ToTimeSpan(); }
        set { ArrivalTime = value.HasValue ? TimeOnly.FromTimeSpan(value.Value) : null; }
    }

    [property: JsonConverter(typeof(TimeOnlyConverter))]
    public TimeOnly? DepartureTime { get; set; }

    public TimeSpan? EndTime
    {
        get { return DepartureTime?.ToTimeSpan(); }
        set { DepartureTime = value.HasValue ? TimeOnly.FromTimeSpan(value.Value) : null; }
    }

    public StationDto StartStation { get; set; } = null!;

    public StationDto EndStation { get; set; } = null!;
}