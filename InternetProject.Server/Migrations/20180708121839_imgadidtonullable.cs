using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetProject.Server.Migrations
{
    public partial class imgadidtonullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Ads_AdID",
                table: "Images");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdID",
                table: "Images",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Ads_AdID",
                table: "Images",
                column: "AdID",
                principalTable: "Ads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Ads_AdID",
                table: "Images");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdID",
                table: "Images",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Ads_AdID",
                table: "Images",
                column: "AdID",
                principalTable: "Ads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
