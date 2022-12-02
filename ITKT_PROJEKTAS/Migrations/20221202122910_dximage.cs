using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITKT_PROJEKTAS.Migrations
{
    public partial class dximage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PictureBytes = table.Column<byte[]>(type: "longblob", nullable: false),
                    PictureFormat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserUserId = table.Column<int>(type: "int", nullable: true),
                    RouteRouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Route_RouteRouteId",
                        column: x => x.RouteRouteId,
                        principalTable: "Route",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Picture_Users_UserUserId",
                        column: x => x.UserUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_RouteRouteId",
                table: "Picture",
                column: "RouteRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_UserUserId",
                table: "Picture",
                column: "UserUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");
        }
    }
}
