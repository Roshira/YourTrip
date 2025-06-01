using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JsonPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExternalPlacesId",
                table: "SavedPlaces",
                newName: "PlaceJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlaceJson",
                table: "SavedPlaces",
                newName: "ExternalPlacesId");
        }
    }
}
