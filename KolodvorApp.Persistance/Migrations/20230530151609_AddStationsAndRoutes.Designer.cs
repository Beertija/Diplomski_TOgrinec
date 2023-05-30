﻿// <auto-generated />
using System;
using KolodvorApp.Persistance.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KolodvorApp.Persistance.Migrations
{
    [DbContext(typeof(KolodvorAppContext))]
    [Migration("20230530151609_AddStationsAndRoutes")]
    partial class AddStationsAndRoutes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Contains", b =>
                {
                    b.Property<Guid>("TrainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrainCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TrainId", "TrainCategoryId");

                    b.HasIndex("TrainCategoryId");

                    b.ToTable("Contains", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("FirstStation")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDaily")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("LastStation")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Routes", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.RouteStation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Arrival")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Departure")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RouteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrainId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("StationId");

                    b.HasIndex("TrainId", "StationId", "RouteId")
                        .IsUnique();

                    b.ToTable("RouteStations", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.RouteTrain", b =>
                {
                    b.Property<Guid>("TrainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RouteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TrainId", "RouteId");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteTrains", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Station", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stations", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Train", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("Id");

                    b.HasIndex("Tag")
                        .IsUnique();

                    b.ToTable("Trains", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.TrainCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TrainCategories", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.TrainMaintenance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Maintenance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TrainId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.ToTable("TrainMaintenances", (string)null);
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Contains", b =>
                {
                    b.HasOne("KolodvorApp.Domain.Entities.TrainCategory", "TrainCategory")
                        .WithMany("Contains")
                        .HasForeignKey("TrainCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KolodvorApp.Domain.Entities.Train", "Train")
                        .WithMany("Categories")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Train");

                    b.Navigation("TrainCategory");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.RouteStation", b =>
                {
                    b.HasOne("KolodvorApp.Domain.Entities.Route", "Route")
                        .WithMany("RouteStations")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KolodvorApp.Domain.Entities.Station", "Station")
                        .WithMany("RouteStations")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.RouteTrain", b =>
                {
                    b.HasOne("KolodvorApp.Domain.Entities.Route", "Route")
                        .WithMany("RouteTrains")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KolodvorApp.Domain.Entities.Train", "Train")
                        .WithMany("RouteTrains")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Train");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.TrainMaintenance", b =>
                {
                    b.HasOne("KolodvorApp.Domain.Entities.Train", "Train")
                        .WithMany("Maintenances")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Train");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Route", b =>
                {
                    b.Navigation("RouteStations");

                    b.Navigation("RouteTrains");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Station", b =>
                {
                    b.Navigation("RouteStations");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Train", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Maintenances");

                    b.Navigation("RouteTrains");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.TrainCategory", b =>
                {
                    b.Navigation("Contains");
                });
#pragma warning restore 612, 618
        }
    }
}
