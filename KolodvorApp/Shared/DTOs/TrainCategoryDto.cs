namespace KolodvorApp.Shared.DTOs;

public class TrainCategoryDto
{
    public Guid? Id { get; set; }

    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return Name;
    }
}