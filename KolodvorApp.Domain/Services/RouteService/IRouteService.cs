using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface IRouteService
{
    List<RouteDto> GetAll();

    List<RouteStationDto> GetAllRouteStations();

    Task<RouteDto> CreateOrUpdateAsync(RouteDto routeDto);

    Task DeleteAsync(Guid id);
}