using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicPetEMR.Migrations
{
    /// <inheritdoc />
    public partial class AddedMedicationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dosage",
                table: "Medications");

            migrationBuilder.AddColumn<int>(
                name: "DoseType",
                table: "Medications",
                type: "INTEGER",
                maxLength: 160,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DoseValue",
                table: "Medications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Medications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Medications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Medications",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "Medications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseType",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DoseValue",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "Medications");

            migrationBuilder.AddColumn<string>(
                name: "Dosage",
                table: "Medications",
                type: "TEXT",
                maxLength: 160,
                nullable: true);
        }
    }
}
