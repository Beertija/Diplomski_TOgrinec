﻿namespace KolodvorApp.Domain.Entities;

public class Train : BaseEntity
{
    public string Tag { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<TrainMaintenance> Maintenances { get; set; }
}