using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Routes",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Routes",
                type: "numeric(3,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "numeric(3,2)");

            migrationBuilder.AddColumn<string>(
                name: "RouteUtl",
                table: "Routes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteUtl",
                table: "Routes");

            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Routes",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Routes",
                type: "numeric(3,2)",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "numeric(3,2)",
                oldNullable: true);
        }
    }
}
