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
                    Voltage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.IdTag);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
