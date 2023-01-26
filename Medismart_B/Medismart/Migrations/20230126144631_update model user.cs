using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medismart.Migrations
{
    public partial class updatemodeluser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "DateBirth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBirth",
                table: "Users",
                newName: "CreatedAt");
        }
    }
}
