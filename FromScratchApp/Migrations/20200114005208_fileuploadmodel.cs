using Microsoft.EntityFrameworkCore.Migrations;

namespace FromScratchApp.Migrations
{
    public partial class fileuploadmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Notes",
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
