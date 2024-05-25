using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemperatureF = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ZipCodesSearch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ForecastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "ForecastZipCodes",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    WeatherForecastKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastZipCodes", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ForecastZipCodes_Forecasts_WeatherForecastKey",
                        column: x => x.WeatherForecastKey,
                        principalTable: "Forecasts",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForecastZipCodes_WeatherForecastKey",
                table: "ForecastZipCodes",
                column: "WeatherForecastKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForecastZipCodes");

            migrationBuilder.DropTable(
                name: "Forecasts");
        }
    }
}
