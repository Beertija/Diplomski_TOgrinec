namespace KolodvorApp.Domain.Entities;

public class Contains
{
    public Guid TrainId { get; set; }

    public Train Train { get; set; } = null!;

    public Guid TrainCategoryId { get; set; }

    public TrainCategory TrainCategory { get; set; } = null!;
}