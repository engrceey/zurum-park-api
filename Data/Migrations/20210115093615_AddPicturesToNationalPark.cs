using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZurumPark.Migrations
{
    public partial class AddPicturesToNationalPark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Pictures",
                table: "NationalParks",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "NationalParks");
        }
    }
}
