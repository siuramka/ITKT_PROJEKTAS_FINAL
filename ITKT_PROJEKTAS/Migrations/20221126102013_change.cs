using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITKT_PROJEKTAS.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ReservationCost",
                table: "Reservation",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationCost",
                table: "Reservation");
        }
    }
}
