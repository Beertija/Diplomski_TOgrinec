﻿using KolodvorApp.Domain.Entities;
using KolodvorApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace KolodvorApp.Persistance.DataContext;

public sealed class KolodvorAppContext : DbContext
{
    public KolodvorAppContext(DbContextOptions<KolodvorAppContext> options) : base(options) { }

    public DbSet<Train> Trains { get; set; }
    public DbSet<TrainMaintenance> TrainMaintenances { get; set; }
    public DbSet<TrainCategory> TrainCategories { get; set; }
    public DbSet<Contains> Contains { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<RouteStation> RouteStations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

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

            b.Navigation(x => x.Categories)
                .AutoInclude();
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

        modelBuilder.Entity<TrainCategory>(b =>
        {
            b.ToTable("TrainCategories");

            b.Property(x => x.Name)
                .IsRequired();
        });

        modelBuilder.Entity<Contains>(b =>
        {
            b.ToTable("Contains");

            b.HasKey(x => new { x.TrainId, x.TrainCategoryId });

            b.HasOne(x => x.Train)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.TrainId)
                .IsRequired();

            b.HasOne(x => x.TrainCategory)
                .WithMany(x => x.Contains)
                .HasForeignKey(x => x.TrainCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Station>(b =>
        {
            b.ToTable("Stations");

            b.Property(x => x.Name)
                .IsRequired();

            b.HasMany(x => x.StartRouteStations)
                .WithOne(x => x.StartStation)
                .HasForeignKey(x => x.StartStationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.HasMany(x => x.EndRouteStations)
                .WithOne(x => x.EndStation)
                .HasForeignKey(x => x.EndStationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        });

        modelBuilder.Entity<RouteStation>(b =>
        {
            b.ToTable("RouteStations");

            b.HasIndex(x => new { x.StartStationId, x.EndStationId, x.RouteId, x.Order })
                .IsUnique();

            b.Property(x => x.Order)
                .IsRequired();

            b.Property(x => x.Cost)
                .HasPrecision(18, 2)
                .IsRequired();

            b.Property(x => x.Arrival)
                .IsRequired();

            b.Property(x => x.Departure)
                .IsRequired();

            b.Ignore(x => x.ArrivalTime);

            b.Ignore(x => x.DepartureTime);

            b.Navigation(x => x.StartStation)
                .AutoInclude();

            b.Navigation(x => x.EndStation)
                .AutoInclude();

            b.HasOne(x => x.Route)
                .WithMany(x => x.RouteStations)
                .HasForeignKey(x => x.RouteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        modelBuilder.Entity<Route>(b =>
        {
            b.ToTable("Routes");

            b.Property(x => x.IsDaily)
                .HasDefaultValue(false)
                .IsRequired();

            b.Navigation(x => x.RouteStations)
                .AutoInclude();

            b.Navigation(x => x.Train)
                .AutoInclude();

            b.HasOne(x => x.Train)
                .WithMany(x => x.Routes)
                .HasForeignKey(x => x.TrainId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("Users");

            b.HasIndex(x => x.Email)
                .IsUnique();

            b.Property(x => x.Name)
                .IsRequired();

            b.Property(x => x.Password)
                .IsRequired();

            b.Property(x => x.Email)
                .IsRequired();

            b.Property(x => x.Role)
                .HasDefaultValue(RoleEnum.User)
                .IsRequired();
        });

        modelBuilder.Entity<Ticket>(b =>
        {
            b.ToTable("Tickets");

            b.Property(x => x.StartStation)
                .IsRequired();

            b.Property(x => x.EndStation)
                .IsRequired();

            b.Property(x => x.Cost)
                .HasPrecision(18, 2)
                .IsRequired();

            b.HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}