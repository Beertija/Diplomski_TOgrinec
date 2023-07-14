namespace KolodvorApp.Shared.DTOs;

public class RouteSearchDto
{
    public DateTime? Date { get; set; } = DateTime.Now;

    public StationDto StartStation { get; set; } = null!;

    public StationDto EndStation { get; set; } = null!;
}