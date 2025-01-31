using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SpeciesRepository : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "species");

            migrationBuilder.DropColumn(
                name: "name",
                table: "breeds");

            migrationBuilder.AddColumn<string>(
                name: "name_value",
                table: "species",
                type: "name",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name_value",
                table: "breeds",
                type: "name",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name_value",
                table: "species");

            migrationBuilder.DropColumn(
                name: "name_value",
                table: "breeds");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "species",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "breeds",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
