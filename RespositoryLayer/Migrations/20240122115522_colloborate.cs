using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespositoryLayer.Migrations
{
    public partial class colloborate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.CreateTable(
                name: "colloborate",
                columns: table => new
                {
                    c_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    notesId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colloborate", x => x.c_id);
                    table.ForeignKey(
                        name: "FK_colloborate_notes_notesId",
                        column: x => x.notesId,
                        principalTable: "notes",
                        principalColumn: "notesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_colloborate_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_colloborate_notesId",
                table: "colloborate",
                column: "notesId");

            migrationBuilder.CreateIndex(
                name: "IX_colloborate_UserId",
                table: "colloborate",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "colloborate");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");
        }
    }
}
