namespace KolodvorApp.Shared.DTOs;

public class MergedRoutesDto
{
    public TimeOnly StartTime => RouteStations.First().DepartureTime;

    public TimeOnly EndTime => RouteStations.Last().ArrivalTime;

    public TimeSpan Duration
    {
        get
        {
            TimeSpan duration;
            if (StartTime > EndTime)
            {
                duration = (TimeSpan.FromHours(24) - StartTime.ToTimeSpan()) + EndTime.ToTimeSpan();
            }
            else
            {
                duration = EndTime.ToTimeSpan() - StartTime.ToTimeSpan();
            }
            return duration;
        }
    }

    public bool ShowDetails { get; set; } = false;

    public string TrainTag => TrainTags.First();

    public int Transfers => TrainTags.Distinct().ToList().Count - 1;

    public decimal Cost
    {
        get
        {
            var total = RouteStations.Sum(x => x.Cost);
            var discount = (decimal)(0.04 * RouteStations.Count);
            if (discount > 0.7m) discount = 0.7m;
            return total * (1 - discount);
        }
    }

    public List<string> TrainTags { get; set; } = new();

    public List<RouteStationDto> RouteStations { get; set; } = new();

    public Dictionary<Guid, string> RouteStationTrainConnection
    {
        get
        {
            var routeStationTrainConnection = new Dictionary<Guid, string>();

            for (int i = 0; i < RouteStations.Count; i++)
            {
                routeStationTrainConnection[RouteStations[i].Id] = TrainTags[i];
            }

            return routeStationTrainConnection;
        }
    }
}