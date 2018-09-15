using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetProject.Server.Migrations
{
    public partial class clear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropColumn(
                name: "CarDescription",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Ads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarDescription",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ads",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Ads",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Ads",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FromID = table.Column<Guid>(nullable: false),
                    FromName = table.Column<string>(maxLength: 20, nullable: false),
                    Path = table.Column<string>(maxLength: 50, nullable: false),
                    ToID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Chats_Accounts_ToID",
                        column: x => x.ToID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ToID",
                table: "Chats",
                column: "ToID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_FromID_ToID",
                table: "Chats",
                columns: new[] { "FromID", "ToID" },
                unique: true);
        }
    }
}
