using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ADW1115HOT2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescShort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescLong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductQty = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Wheels" },
                    { 2, "Frames" },
                    { 3, "Handlebars" },
                    { 4, "Saddles" },
                    { 5, "Brakes" },
                    { 6, "Drivetrains" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "ProductDescLong", "ProductDescShort", "ProductImage", "ProductName", "ProductPrice", "ProductQty" },
                values: new object[,]
                {
                    { 1, 1, "The AeroFlo ATB wheels are designed for rugged trails with lightweight aluminum construction.", "Durable mountain bike wheels", "aeroflo-atb.jpg", "AeroFlo ATB Wheels", 250.00m, 10 },
                    { 2, 2, "TrailBlazer provides strength and comfort for cross-country adventures.", "Lightweight alloy frame", "trailblazer-frame.jpg", "TrailBlazer Frame", 500.00m, 5 },
                    { 3, 3, "Designed for maximum grip and aerodynamic performance on road bikes.", "Ergonomic racing handlebars", "ergopro-handlebars.jpg", "ErgoPro Handlebars", 120.00m, 15 },
                    { 4, 4, "ComfortRide saddles provide lasting comfort for long-distance riders.", "High-density foam saddle", "comfortride-saddle.jpg", "ComfortRide Saddle", 75.00m, 20 },
                    { 5, 5, "Reliable stopping power for road and mountain bikes with easy adjustment features.", "High-performance brake system", "prostop-brakes.jpg", "ProStop Brakes", 90.00m, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
