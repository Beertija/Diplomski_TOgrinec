namespace KolodvorApp.Shared.DTOs;

public class TrainSelectorDto
{
    public Guid Id { get; set; }

    public string Tag { get; set; } = null!;

    public int Capacity { get; set; }

    public override string ToString()
    {
        return Tag;
    }
}