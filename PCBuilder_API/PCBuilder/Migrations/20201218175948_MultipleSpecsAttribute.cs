using Microsoft.EntityFrameworkCore.Migrations;

namespace PCBuilder.Migrations
{
    public partial class MultipleSpecsAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Multiple",
                table: "Specifications",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Multiple",
                table: "Specifications");
        }
    }
}
