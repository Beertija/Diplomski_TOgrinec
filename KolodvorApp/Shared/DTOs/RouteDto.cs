namespace KolodvorApp.Shared.DTOs;

public class RouteDto
{
    public Guid? Id { get; set; }

    public bool IsDaily { get; set; }

    public string? StartStation { get; set; }

    public string? EndStation { get; set; }

    public Guid TrainId { get; set; }

    public string? TrainTag { get; set; }

    public List<RouteStationDto> RouteStations { get; set; } = new();
}