using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespositoryLayer.Migrations
{
    public partial class gri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePaths",
                table: "notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePaths",
                table: "notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
