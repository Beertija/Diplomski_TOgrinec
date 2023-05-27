namespace KolodvorApp.Shared.DTOs;

public class TrainDto
{
    public Guid? Id { get; set; }

    public string Tag { get; set; } = null!;

    public int Capacity { get; set; }

    public List<TrainMaintenanceDto>? Maintenances { get; set; }
}