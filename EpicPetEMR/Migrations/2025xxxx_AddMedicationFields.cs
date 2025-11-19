using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicPetEMR.Api.Migrations
{
    public partial class AddMedicationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DoseValue",
                table: "Medications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoseType",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Unknown");

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Unknown");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Medications",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Medications",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Medications",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseValue",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DoseType",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Route",
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
                name: "StartDate",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Medications");
        }
    }
}
