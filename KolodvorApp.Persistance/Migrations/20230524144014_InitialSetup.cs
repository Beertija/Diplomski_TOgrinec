using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KolodvorApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainMaintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Maintenance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TrainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainMaintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainMaintenances_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainMaintenances_TrainId",
                table: "TrainMaintenances",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_Tag",
                table: "Trains",
                column: "Tag",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainMaintenances");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
