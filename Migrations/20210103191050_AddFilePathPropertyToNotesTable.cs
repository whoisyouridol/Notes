using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Migrations
{
    public partial class AddFilePathPropertyToNotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Notes",
                type: "nvarchar(150)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Notes");
        }
    }
}
