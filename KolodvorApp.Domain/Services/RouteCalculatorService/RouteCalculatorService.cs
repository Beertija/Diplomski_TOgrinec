using KolodvorApp.Shared.DTOs;

namespace KolodvorApp.Domain.Services;

public class RouteCalculatorService : IRouteCalculatorService
{
    private readonly IRouteService _routeService;

    public RouteCalculatorService(IRouteService routeService)
    {
        _routeService = routeService;
    }

    public List<MergedRoutesDto> FindTravelPaths(RouteSearchDto routeSearch)
    {
        var routes = _routeService.GetAll();
        var filteredRoutes = IsRouteAvailable(routes, (DateTime)routeSearch.Date!);
        var routeSegments = PrepareRouteSegments(filteredRoutes);
        var travelPaths = new List<MergedRoutesDto>();

        foreach (var segment in routeSegments)
        {
            if (segment.RouteStation.StartStation!.Id == routeSearch.StartStation.Id)
            {
                var currentPath = new List<RouteSegmentDto>();
                DepthFirstSearch(segment, routeSearch.EndStation, currentPath, travelPaths, routeSegments);
            }
        }

        return travelPaths;
    }

    private static void DepthFirstSearch(RouteSegmentDto currentSegment, StationDto endStation, List<RouteSegmentDto> currentPath, List<MergedRoutesDto> travelPaths, List<RouteSegmentDto> routeSegments)
    {
        currentPath.Add(currentSegment);

        if (currentSegment.RouteStation.EndStation!.Id == endStation.Id)
        {
            var mergedRoute = new MergedRoutesDto();
            foreach (var routeSegment in currentPath)
            {
                mergedRoute.TrainTags.Add(routeSegment.Route.TrainTag!);
                mergedRoute.RouteStations.Add(routeSegment.RouteStation);
            }
            travelPaths.Add(mergedRoute);
        }
        else
        {
            foreach (var nextSegment in GetNextSegments(routeSegments, currentSegment.RouteStation.EndStation))
            {
                if (currentPath.All(x => x.Route == nextSegment.Route))
                {
                    DepthFirstSearch(nextSegment, endStation, currentPath, travelPaths, routeSegments);
                }
            }
        }

        currentPath.RemoveAt(currentPath.Count - 1);
    }

    private static List<RouteDto> IsRouteAvailable(List<RouteDto> routes, DateTime dateOfTravel)
    {
        return routes.Where(route => route.IsDaily 
            || !(dateOfTravel.DayOfWeek == DayOfWeek.Saturday || dateOfTravel.DayOfWeek == DayOfWeek.Sunday))
            .ToList();
    }

    private static List<RouteSegmentDto> PrepareRouteSegments(List<RouteDto> filteredRoutes)
    {
        var routeSegments = new List<RouteSegmentDto>();
        foreach (var route in filteredRoutes)
        {
            var routeStations = route.RouteStations.OrderBy(x => x.Order).ToList();
            foreach (var routeStation in routeStations)
            {
                var routeSegment = new RouteSegmentDto
                {
                    Route = route,
                    RouteStation = routeStation
                };
                routeSegments.Add(routeSegment);
            }
        }
        return routeSegments;
    }

    private static List<RouteSegmentDto> GetNextSegments(List<RouteSegmentDto> routeSegments, StationDto endStation)
    {
        return routeSegments.Where(x => x.RouteStation.StartStationId == endStation.Id).ToList();
    }
}