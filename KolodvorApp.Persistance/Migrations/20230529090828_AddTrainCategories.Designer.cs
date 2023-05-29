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
    [Migration("20230529090828_AddTrainCategories")]
    partial class AddTrainCategories
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

            modelBuilder.Entity("KolodvorApp.Domain.Entities.TrainMaintenance", b =>
                {
                    b.HasOne("KolodvorApp.Domain.Entities.Train", "Train")
                        .WithMany("Maintenances")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Train");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.Train", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Maintenances");
                });

            modelBuilder.Entity("KolodvorApp.Domain.Entities.TrainCategory", b =>
                {
                    b.Navigation("Contains");
                });
#pragma warning restore 612, 618
        }
    }
}
