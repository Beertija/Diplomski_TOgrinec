namespace KolodvorApp.Shared.DTOs;

public class ContainsDto
{
    public Guid TrainId { get; set; }

    public TrainDto? Train { get; set; }

    public Guid TrainCategoryId { get; set; }

    public TrainCategoryDto? TrainCategory { get; set; }
}