using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface IRouteService
{
    List<RouteDto> GetAll();

    Task<RouteDto> CreateOrUpdateAsync(RouteDto routeDto);

    Task DeleteAsync(Guid id);
}