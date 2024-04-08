using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace product_identification_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true),
                    ProductType = table.Column<string>(type: "TEXT", nullable: false),
                    IntroductionDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    SalesDiscontinuationDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    SupportDiscontinuationDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlternateIdentities",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdentityType = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateIdentities", x => new { x.ProductId, x.IdentityType });
                    table.ForeignKey(
                        name: "FK_AlternateIdentities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternateIdentities");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
