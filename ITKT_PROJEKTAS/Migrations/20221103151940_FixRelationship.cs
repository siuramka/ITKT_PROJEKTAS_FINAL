using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITKT_PROJEKTAS.Migrations
{
    public partial class FixRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Route_Reservation_ReservationId",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Route_ReservationId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Route");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RouteId",
                table: "Reservation",
                column: "RouteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Route_RouteId",
                table: "Reservation",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Route_RouteId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_RouteId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Route",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Route_ReservationId",
                table: "Route",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Reservation_ReservationId",
                table: "Route",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
