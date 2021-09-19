using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace PCBuilder.Migrations
{
    public partial class AdvertPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                nullable: false,
                defaultValue: "Seller",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "Seller");

            migrationBuilder.CreateTable(
                name: "AdvertPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: true),
                    AdvertId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertPhotos_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertPhotos_AdvertId",
                table: "AdvertPhotos",
                column: "AdvertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertPhotos");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: true,
                defaultValue: "Seller",
                oldClrType: typeof(string),
                oldDefaultValue: "Seller");
        }
    }
}
