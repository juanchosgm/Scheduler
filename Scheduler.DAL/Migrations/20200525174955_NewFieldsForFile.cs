using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.DAL.Migrations
{
    public partial class NewFieldsForFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileDescription",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileDescription",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Appointments");
        }
    }
}
