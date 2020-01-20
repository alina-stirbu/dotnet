using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.Data.Migrations
{
    public partial class RateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Rates",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "Rates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Rates");
        }
    }
}
