using Microsoft.EntityFrameworkCore.Migrations;

namespace StockExchangeMVC.Migrations
{
    public partial class Indexnameadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Range",
                table: "DayTickWSE");

            migrationBuilder.AddColumn<string>(
                name: "IndexName",
                table: "DayTickWSE",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndexName",
                table: "DayTickWSE");

            migrationBuilder.AddColumn<decimal>(
                name: "Range",
                table: "DayTickWSE",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
