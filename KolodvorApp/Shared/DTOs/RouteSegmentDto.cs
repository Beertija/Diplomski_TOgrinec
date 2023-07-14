namespace KolodvorApp.Shared.DTOs;

public class RouteSegmentDto
{
    public RouteDto Route { get; set; } = new();

    public RouteStationDto RouteStation { get; set; } = new();
}