using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace PCBuilder.Migrations
{
    public partial class ChangedSellerToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameTable(
                name: "Sellers",
                newName: "Users"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Sellers"
                );
        }
    }
}
