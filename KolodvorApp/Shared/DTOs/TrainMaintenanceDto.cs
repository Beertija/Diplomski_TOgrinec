namespace KolodvorApp.Shared.DTOs;

public class TrainMaintenanceDto
{
    public Guid? Id { get; set; }

    public string Maintenance { get; set; } = null!;

    public decimal Cost { get; set; }

    public Guid? TrainId { get; set; }
}