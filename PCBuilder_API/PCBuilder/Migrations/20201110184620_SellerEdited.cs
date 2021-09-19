using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCBuilder.Migrations
{
    public partial class SellerEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Sellers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Sellers");
        }
    }
}
