﻿using KolodvorApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KolodvorApp.Persistance.DataContext;

public sealed class KolodvorAppContext : DbContext
{
    public KolodvorAppContext(DbContextOptions<KolodvorAppContext> options) : base(options) { }

    public DbSet<Train> Trains { get; set; }
    public DbSet<TrainMaintenance> TrainMaintenances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Train>(b =>
        {
            b.ToTable("Trains");

            b.Property(x => x.Tag)
                .HasMaxLength(4)
                .IsRequired();

            b.Property(x => x.Capacity)
                .IsRequired();

            b.HasIndex(x => new { x.Tag })
                .IsUnique();
        });

        modelBuilder.Entity<TrainMaintenance>(b =>
        {
            b.ToTable("TrainMaintenances");

            b.Property(x => x.Maintenance)
                .IsRequired();

            b.Property(x => x.Cost)
                .HasPrecision(18, 2)
                .IsRequired();

            b.HasOne(x => x.Train)
                .WithMany(x => x.Maintenances)
                .HasForeignKey(x => x.TrainId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}