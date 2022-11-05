using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITKT_PROJEKTAS.Migrations
{
    public partial class updatedbz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeLength",
                table: "Route",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Route",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Route");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Route",
                newName: "TimeLength");
        }
    }
}
