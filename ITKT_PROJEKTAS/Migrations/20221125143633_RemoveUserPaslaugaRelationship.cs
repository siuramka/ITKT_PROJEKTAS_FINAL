using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITKT_PROJEKTAS.Migrations
{
    public partial class RemoveUserPaslaugaRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paslauga_Users_UserIdManagerId",
                table: "Paslauga");

            migrationBuilder.DropIndex(
                name: "IX_Paslauga_UserIdManagerId",
                table: "Paslauga");

            migrationBuilder.DropColumn(
                name: "UserIdManagerId",
                table: "Paslauga");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserIdManagerId",
                table: "Paslauga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paslauga_UserIdManagerId",
                table: "Paslauga",
                column: "UserIdManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paslauga_Users_UserIdManagerId",
                table: "Paslauga",
                column: "UserIdManagerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
