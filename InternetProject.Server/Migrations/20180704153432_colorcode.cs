using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetProject.Server.Migrations
{
    public partial class colorcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Colors",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_Code",
                table: "Colors",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_Color",
                table: "Colors",
                column: "Color",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colors_Code",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Colors_Color",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Colors");
        }
    }
}
