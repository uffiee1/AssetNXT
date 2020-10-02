using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ruuvi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    IdLocation = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Accuracy = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.IdLocation);
                });

            migrationBuilder.CreateTable(
                name: "RuuviStations",
                columns: table => new
                {
                    IdStation = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BatteryLevel = table.Column<int>(nullable: false),
                    DeviceId = table.Column<string>(maxLength: 250, nullable: false),
                    EventId = table.Column<string>(maxLength: 250, nullable: false),
                    LocationIdLocation = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuuviStations", x => x.IdStation);
                    table.ForeignKey(
                        name: "FK_RuuviStations_Locations_LocationIdLocation",
                        column: x => x.LocationIdLocation,
                        principalTable: "Locations",
                        principalColumn: "IdLocation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    IdTag = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccelX = table.Column<double>(nullable: false),
                    AccelY = table.Column<double>(nullable: false),
                    AccelZ = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DataFormat = table.Column<int>(nullable: false),
                    DefaultBackground = table.Column<int>(nullable: false),
                    Favorite = table.Column<bool>(nullable: false),
                    Humidity = table.Column<double>(nullable: false),
                    HumidityOffset = table.Column<double>(nullable: false),
                    Id = table.Column<string>(maxLength: 250, nullable: false),
                    MeasurementSequenceNumber = table.Column<int>(nullable: false),
                    MovementCounter = table.Column<int>(nullable: false),
                    Pressure = table.Column<long>(nullable: false),
                    Rssi = table.Column<int>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    txPower = table.Column<int>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    Voltage = table.Column<double>(nullable: false),
                    RuuviStationIdStation = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.IdTag);
                    table.ForeignKey(
                        name: "FK_Tags_RuuviStations_RuuviStationIdStation",
                        column: x => x.RuuviStationIdStation,
                        principalTable: "RuuviStations",
                        principalColumn: "IdStation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RuuviStations_LocationIdLocation",
                table: "RuuviStations",
                column: "LocationIdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_RuuviStationIdStation",
                table: "Tags",
                column: "RuuviStationIdStation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "RuuviStations");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
