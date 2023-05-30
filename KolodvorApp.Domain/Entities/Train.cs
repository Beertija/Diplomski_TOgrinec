namespace KolodvorApp.Domain.Entities;

public class Train : BaseEntity
{
    public string Tag { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<TrainMaintenance> Maintenances { get; set; } = new List<TrainMaintenance>();

    public virtual ICollection<Contains> Categories { get; set; } = new List<Contains>();

    public virtual ICollection<RouteTrain> RouteTrains { get; set; } = new List<RouteTrain>();
}