using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public interface IRouteCalculatorService
{
    List<MergedRoutesDto> FindTravelPaths(RouteSearchDto routeSearch);
}