using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteEntityCanBeNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
        name: "Review",
        table: "Routes",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Routes",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
