using Microsoft.EntityFrameworkCore.Migrations;
using YourTrips.Core.Entities.Achievement;

#nullable disable

namespace YourTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeIconUrlNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SavedYourMemoirs",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
