using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteUtl",
                table: "Routes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RouteUtl",
                table: "Routes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
