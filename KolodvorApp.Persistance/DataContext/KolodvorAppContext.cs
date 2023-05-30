using KolodvorApp.Domain.Entities;
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
    public DbSet<RouteTrain> RouteTrains { get; set; }

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
        });

        modelBuilder.Entity<RouteStation>(b =>
        {
            b.ToTable("RouteStations");

            b.HasIndex(x => new { x.TrainId, x.StationId, x.RouteId})
                .IsUnique();

            b.Property(x => x.TrainId)
                .IsRequired();

            b.Ignore(x => x.ArrivalTime);

            b.Ignore(x => x.DepartureTime);

            b.HasOne(x => x.Station)
                .WithMany(x => x.RouteStations)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

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

            b.Property(x => x.Cost)
                .HasPrecision(18, 2)
                .IsRequired();

            b.Property(x => x.FirstStation)
                .IsRequired();

            b.Property(x => x.LastStation)
                .IsRequired();
        });

        modelBuilder.Entity<RouteTrain>(b =>
        {
            b.ToTable("RouteTrains");

            b.HasKey(x => new { x.TrainId, x.RouteId });

            b.HasOne(x => x.Train)
                .WithMany(x => x.RouteTrains)
                .HasForeignKey(x => x.TrainId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.HasOne(x => x.Route)
                .WithMany(x => x.RouteTrains)
                .HasForeignKey(x => x.RouteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });
    }
}