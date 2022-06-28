using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nubex_DataAccess.Migrations
{
    public partial class AddProductToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetalWeight = table.Column<double>(type: "float", nullable: false),
                    MetalBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Purify = table.Column<double>(type: "float", nullable: false),
                    Manufacture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTax = table.Column<bool>(type: "bit", nullable: false),
                    Featured = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    ProductTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remark_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remark_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remark_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
