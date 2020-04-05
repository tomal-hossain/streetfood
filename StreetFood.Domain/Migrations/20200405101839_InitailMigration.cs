using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetFood.Domain.Migrations
{
    public partial class InitailMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Pasword = table.Column<string>(type: "nvarchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "date", nullable: false),
                    AddedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_User_AddedById",
                        column: x => x.AddedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryFood",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryFood_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bangladesh" },
                    { 2, "India" },
                    { 3, "Srilanka" },
                    { 4, "Pakistan" },
                    { 5, "Bhutan" },
                    { 6, "China" },
                    { 7, "Japan" },
                    { 8, "Nepal" },
                    { 9, "Qatar" },
                    { 10, "Singapore" },
                    { 11, "Malaysia" },
                    { 12, "Thailand" },
                    { 13, "Australia" },
                    { 14, "Canada" },
                    { 15, "USA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryFood_CountryId",
                table: "CountryFood",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryFood_FoodId",
                table: "CountryFood",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_AddedById",
                table: "Food",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryFood");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
