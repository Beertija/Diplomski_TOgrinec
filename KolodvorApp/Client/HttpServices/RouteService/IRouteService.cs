using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface IRouteService
{
    Task<List<RouteDto>> GetAll();

    Task<HttpResponseMessage> DeleteRoute(Guid routeId);
}