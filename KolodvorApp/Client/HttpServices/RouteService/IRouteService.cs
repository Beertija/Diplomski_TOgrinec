using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Client.HttpServices;

public interface IRouteService
{
    Task<List<RouteDto>> GetAll();

    Task<HttpResponseMessage> CreateOrUpdateRoute(RouteDto route);

    Task<HttpResponseMessage> DeleteRoute(Guid routeId);

    Task<HttpResponseMessage> SearchRoutes(RouteSearchDto searchInfo);
}