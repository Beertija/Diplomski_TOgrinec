using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface IRouteService
{
    List<RouteDto> GetAll();

    List<RouteStationDto> GetAllRouteStations();
}