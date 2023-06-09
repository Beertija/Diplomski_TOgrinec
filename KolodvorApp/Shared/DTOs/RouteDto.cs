namespace KolodvorApp.Shared.DTOs;

public class RouteDto
{
    public Guid Id { get; set; }

    public bool IsDaily { get; set; }

    public Guid TrainId { get; set; }

    public List<RouteStationDto> RouteStations { get; set; } = new List<RouteStationDto>();
}