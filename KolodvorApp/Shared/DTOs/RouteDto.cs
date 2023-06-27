namespace KolodvorApp.Shared.DTOs;

public class RouteDto
{
    public Guid Id { get; set; }

    public bool IsDaily { get; set; }

    public string StartStation { get; set; } = null!;

    public string EndStation { get; set; } = null!;

    public Guid TrainId { get; set; }

    public string TrainTag { get; set; } = null!;

    public List<RouteStationDto> RouteStations { get; set; } = new();
}