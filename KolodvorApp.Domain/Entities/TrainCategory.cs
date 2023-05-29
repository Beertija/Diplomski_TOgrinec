namespace KolodvorApp.Domain.Entities;

public class TrainCategory : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Contains> Contains { get; set; } = new List<Contains>();
}