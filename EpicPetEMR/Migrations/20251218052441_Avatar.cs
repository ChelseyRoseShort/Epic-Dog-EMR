using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicPetEMR.Migrations
{
    /// <inheritdoc />
    public partial class Avatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetFindings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PetId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PlacedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RemovedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    Assessment = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoDocumentId = table.Column<int>(type: "INTEGER", nullable: true),
                    MapKey = table.Column<int>(type: "INTEGER", nullable: true),
                    CellIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    GridRows = table.Column<int>(type: "INTEGER", nullable: true),
                    GridCols = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetFindings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetFindings_PetDocuments_PhotoDocumentId",
                        column: x => x.PhotoDocumentId,
                        principalTable: "PetDocuments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PetFindings_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetFindings_PetId",
                table: "PetFindings",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetFindings_PhotoDocumentId",
                table: "PetFindings",
                column: "PhotoDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetFindings");
        }
    }
}
