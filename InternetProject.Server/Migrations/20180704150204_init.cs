using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetProject.Server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Color = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CarName = table.Column<string>(maxLength: 70, nullable: false),
                    BrandID = table.Column<Guid>(nullable: false),
                    Fuel = table.Column<int>(nullable: false),
                    Gearbox = table.Column<int>(nullable: false),
                    CarClass = table.Column<int>(nullable: false),
                    CarDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CityID = table.Column<Guid>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CarID = table.Column<Guid>(nullable: false),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    KM = table.Column<double>(nullable: false),
                    ColorID = table.Column<Guid>(nullable: false),
                    FirstHanded = table.Column<bool>(nullable: false),
                    Insurance = table.Column<bool>(nullable: false),
                    InsuranceExpirationDate = table.Column<DateTime>(nullable: true),
                    TechnicalInspection = table.Column<bool>(nullable: false),
                    PlanedPayment = table.Column<bool>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    AdvancedPayment = table.Column<double>(nullable: true),
                    InstallmentsPayment = table.Column<double>(nullable: true),
                    InstallmentsCount = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerID = table.Column<Guid>(nullable: false),
                    AdTime = table.Column<DateTime>(nullable: false),
                    Expired = table.Column<bool>(nullable: false),
                    Verified = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(maxLength: 300, nullable: true),
                    Lng = table.Column<double>(nullable: true),
                    Lat = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ads_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ads_Colors_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Colors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ads_Accounts_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ToID = table.Column<Guid>(nullable: false),
                    FromName = table.Column<string>(maxLength: 20, nullable: false),
                    FromID = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(maxLength: 50, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    AdID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_Ads_AdID",
                        column: x => x.AdID,
                        principalTable: "Ads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CityID",
                table: "Accounts",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PhoneNumber",
                table: "Accounts",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CarID",
                table: "Ads",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_ColorID",
                table: "Ads",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_OwnerID",
                table: "Ads",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandID",
                table: "Cars",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ToID",
                table: "Chats",
                column: "ToID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_FromID_ToID",
                table: "Chats",
                columns: new[] { "FromID", "ToID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdID",
                table: "Images",
                column: "AdID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
