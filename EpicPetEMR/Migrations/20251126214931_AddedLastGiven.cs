using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicPetEMR.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastGiven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastGiven",
                table: "Medications",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastGiven",
                table: "Medications");
        }
    }
}
