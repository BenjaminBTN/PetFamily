using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetOrdinalNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "pets",
                newName: "despription");

            migrationBuilder.AddColumn<int>(
                name: "ordinal_number",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ordinal_number",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "despription",
                table: "pets",
                newName: "description");
        }
    }
}
