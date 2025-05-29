using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace YourTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedFlights_AspNetUsers_UserId",
                table: "SavedFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedHotels_AspNetUsers_UserId",
                table: "SavedHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPlaces_AspNetUsers_UserId",
                table: "SavedPlaces");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "SavedBlaBlaCarTrips");

            migrationBuilder.DropTable(
                name: "SavedTrainTrips");

            migrationBuilder.DropTable(
                name: "TripHistories");

            migrationBuilder.DropIndex(
                name: "IX_SavedPlaces_UserId",
                table: "SavedPlaces");

            migrationBuilder.DropIndex(
                name: "IX_SavedHotels_UserId",
                table: "SavedHotels");

            migrationBuilder.DropIndex(
                name: "IX_SavedFlights_UserId",
                table: "SavedFlights");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavedPlaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavedHotels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavedFlights");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "SavedPlaces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "SavedHotels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "SavedFlights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Review = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Rating = table.Column<double>(type: "numeric(3,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedPlaces_RouteId",
                table: "SavedPlaces",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedHotels_RouteId",
                table: "SavedHotels",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedFlights_RouteId",
                table: "SavedFlights",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserId",
                table: "Routes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFlights_Routes_RouteId",
                table: "SavedFlights",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedHotels_Routes_RouteId",
                table: "SavedHotels",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPlaces_Routes_RouteId",
                table: "SavedPlaces",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedFlights_Routes_RouteId",
                table: "SavedFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedHotels_Routes_RouteId",
                table: "SavedHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPlaces_Routes_RouteId",
                table: "SavedPlaces");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_SavedPlaces_RouteId",
                table: "SavedPlaces");

            migrationBuilder.DropIndex(
                name: "IX_SavedHotels_RouteId",
                table: "SavedHotels");

            migrationBuilder.DropIndex(
                name: "IX_SavedFlights_RouteId",
                table: "SavedFlights");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "SavedPlaces");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "SavedHotels");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "SavedFlights");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SavedPlaces",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SavedHotels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SavedFlights",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameCountry = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedBlaBlaCarTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalBlaBlaCarId = table.Column<string>(type: "text", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedBlaBlaCarTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedBlaBlaCarTrips_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedTrainTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalTrainId = table.Column<string>(type: "text", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedTrainTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedTrainTrips_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    ExternalBlaBlaCarTripId = table.Column<string>(type: "text", nullable: false),
                    ExternalFlightId = table.Column<string>(type: "text", nullable: false),
                    ExternalHotelId = table.Column<string>(type: "text", nullable: false),
                    ExternalPlaceId = table.Column<string>(type: "text", nullable: false),
                    ExternalTrainTripId = table.Column<string>(type: "text", nullable: false),
                    Finished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedPlaces_UserId",
                table: "SavedPlaces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedHotels_UserId",
                table: "SavedHotels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedFlights_UserId",
                table: "SavedFlights",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedBlaBlaCarTrips_UserId",
                table: "SavedBlaBlaCarTrips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedTrainTrips_UserId",
                table: "SavedTrainTrips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TripHistories_UserId",
                table: "TripHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedFlights_AspNetUsers_UserId",
                table: "SavedFlights",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedHotels_AspNetUsers_UserId",
                table: "SavedHotels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPlaces_AspNetUsers_UserId",
                table: "SavedPlaces",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
