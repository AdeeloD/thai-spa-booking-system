using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MomoAndMemeBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToMasseur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Masseurs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Masseurs");
        }
    }
}
