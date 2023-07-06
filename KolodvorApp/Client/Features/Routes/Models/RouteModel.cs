using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.Features.Routes.Models;

public class RouteModel
{
    public Guid? Id { get; set; }

    public bool IsDaily { get; set; }

    public TrainSelectorDto Train { get; set; } = null!;

    public List<RouteStationModel> RouteStations { get; set; } = new();
}