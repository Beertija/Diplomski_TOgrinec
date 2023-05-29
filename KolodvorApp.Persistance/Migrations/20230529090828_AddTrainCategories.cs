using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KolodvorApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contains",
                columns: table => new
                {
                    TrainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contains", x => new { x.TrainId, x.TrainCategoryId });
                    table.ForeignKey(
                        name: "FK_Contains_TrainCategories_TrainCategoryId",
                        column: x => x.TrainCategoryId,
                        principalTable: "TrainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contains_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contains_TrainCategoryId",
                table: "Contains",
                column: "TrainCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contains");

            migrationBuilder.DropTable(
                name: "TrainCategories");
        }
    }
}
