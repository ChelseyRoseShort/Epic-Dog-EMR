using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicPetEMR.Migrations
{
    /// <inheritdoc />
    public partial class PetMedicalHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetMedicalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PetId = table.Column<int>(type: "INTEGER", nullable: false),
                    Diagnosis = table.Column<string>(type: "TEXT", nullable: false),
                    DiagnosedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ResolvedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetMedicalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetMedicalHistories_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetMedicalHistories_PetId",
                table: "PetMedicalHistories",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetMedicalHistories");
        }
    }
}
