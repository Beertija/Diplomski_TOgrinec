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

        // Initialize priority queue
        var queue = new PriorityQueue<RouteSegmentDto, double>();
        // Maps each node to its minimum cost to reach from start station
        var dist = new Dictionary<StationDto, double>();
        // Maps each node to its previous node in the optimal path from start
        var prev = new Dictionary<StationDto, RouteSegmentDto>();

        foreach (var segment in routeSegments)
        {
            if (segment.RouteStation.StartStation!.Id == routeSearch.StartStation.Id)
            {
                // Initialize start station's cost to reach as 0 and enqueue it
                dist[segment.RouteStation.StartStation] = 0;
                queue.Enqueue(segment, 0);
            }
        }

        while (queue.Count > 0)
        {
            var currentSegment = queue.Dequeue();

            foreach (var nextSegment in GetNextSegments(routeSegments, currentSegment.RouteStation))
            {
                if (!prev.ContainsKey(nextSegment.RouteStation.StartStation!)
                    || currentSegment.RouteStation.DepartureTime <= nextSegment.RouteStation.ArrivalTime)
                {
                    // Update the cost to reach the next station
                    var alt = dist[currentSegment.RouteStation.StartStation!]
                                + (nextSegment.RouteStation.ArrivalTime - currentSegment.RouteStation.DepartureTime).TotalMinutes;

                    if (!dist.ContainsKey(nextSegment.RouteStation.StartStation!) || alt < dist[nextSegment.RouteStation.StartStation!])
                    {
                        dist[nextSegment.RouteStation.StartStation!] = alt;
                        prev[nextSegment.RouteStation.StartStation!] = currentSegment;
                        queue.Enqueue(nextSegment, alt);
                    }
                }
            }
        }

        // Construct all the paths
        var travelPaths = new List<MergedRoutesDto>();
        foreach (var targetSegment in routeSegments.Where(segment => segment.RouteStation.EndStation!.Id == routeSearch.EndStation.Id))
        {
            var path = new List<RouteSegmentDto>();
            var current = targetSegment;

            // Go backwards from the target to the source to construct the path
            while (current != null)
            {
                path.Add(current);
                prev.TryGetValue(current.RouteStation.StartStation!, out current);
            }

            var routeStations = path.Select(x => x.RouteStation).OrderBy(station => station.ArrivalTime).ToList();
            if (routeStations.First().StartStation!.Name == routeSearch.StartStation.Name && routeStations.Last().EndStation!.Name == routeSearch.EndStation.Name)
            {
                travelPaths.Add(new MergedRoutesDto
                {
                    RouteStations = routeStations,
                    TrainTags = path.Select(x => x.Route.TrainTag!).ToList()
                });
            }
        }

        return travelPaths.Where(path => path.Duration <= TimeSpan.FromHours(18)).ToList();
    }


    private static List<RouteDto> IsRouteAvailable(List<RouteDto> routes, DateTime dateOfTravel)
    {
        return routes.Where(route => route.IsDaily
            || !(dateOfTravel.DayOfWeek == DayOfWeek.Saturday || dateOfTravel.DayOfWeek == DayOfWeek.Sunday)).ToList();
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

    private static List<RouteSegmentDto> GetNextSegments(List<RouteSegmentDto> routeSegments, RouteStationDto endStation)
    {
        return routeSegments
            .Where(x => x.RouteStation.StartStationId == endStation.EndStation!.Id
                && x.RouteStation.DepartureTime >= endStation.ArrivalTime)
            .ToList();
    }
}