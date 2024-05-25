using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ForecastsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"     
                CREATE VIEW [dbo].[ForecastsView]
                AS
                SELECT        [Key], TemperatureF, Summary, ZipCodesSearch, ForecastDate, DateAdded, DateUpdated
                FROM            dbo.Forecasts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view ForecastsView");
        }
    }
}
