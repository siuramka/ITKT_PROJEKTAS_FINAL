using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITKT_PROJEKTAS.Migrations
{
    public partial class multiplepaslaugos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Paslauga_PaslaugaId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_PaslaugaId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PaslaugaId",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Route",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaslaugaReservation",
                columns: table => new
                {
                    PaslaugaId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaslaugaReservation", x => new { x.PaslaugaId, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_PaslaugaReservation_Paslauga_PaslaugaId",
                        column: x => x.PaslaugaId,
                        principalTable: "Paslauga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaslaugaReservation_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PaslaugaReservation_ReservationId",
                table: "PaslaugaReservation",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaslaugaReservation");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Route",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PaslaugaId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PaslaugaId",
                table: "Reservation",
                column: "PaslaugaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Paslauga_PaslaugaId",
                table: "Reservation",
                column: "PaslaugaId",
                principalTable: "Paslauga",
                principalColumn: "Id");
        }
    }
}
