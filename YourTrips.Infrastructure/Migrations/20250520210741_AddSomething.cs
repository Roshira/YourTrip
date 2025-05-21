using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSomething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExternalHotelId",
                table: "SavedHotels",
                newName: "HotelJson");

            migrationBuilder.RenameColumn(
                name: "ExternalFlightsId",
                table: "SavedFlights",
                newName: "FlightsJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelJson",
                table: "SavedHotels",
                newName: "ExternalHotelId");

            migrationBuilder.RenameColumn(
                name: "FlightsJson",
                table: "SavedFlights",
                newName: "ExternalFlightsId");
        }
    }
}
