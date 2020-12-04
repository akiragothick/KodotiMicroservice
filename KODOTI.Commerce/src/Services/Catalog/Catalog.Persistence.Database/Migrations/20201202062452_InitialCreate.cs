using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Persistence.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for product 1", "Product 1", 188m },
                    { 73, "Description for product 73", "Product 73", 281m },
                    { 72, "Description for product 72", "Product 72", 439m },
                    { 71, "Description for product 71", "Product 71", 128m },
                    { 70, "Description for product 70", "Product 70", 573m },
                    { 69, "Description for product 69", "Product 69", 964m },
                    { 68, "Description for product 68", "Product 68", 592m },
                    { 67, "Description for product 67", "Product 67", 218m },
                    { 66, "Description for product 66", "Product 66", 849m },
                    { 65, "Description for product 65", "Product 65", 337m },
                    { 64, "Description for product 64", "Product 64", 395m },
                    { 63, "Description for product 63", "Product 63", 437m },
                    { 62, "Description for product 62", "Product 62", 128m },
                    { 61, "Description for product 61", "Product 61", 406m },
                    { 60, "Description for product 60", "Product 60", 443m },
                    { 59, "Description for product 59", "Product 59", 769m },
                    { 58, "Description for product 58", "Product 58", 633m },
                    { 57, "Description for product 57", "Product 57", 924m },
                    { 56, "Description for product 56", "Product 56", 396m },
                    { 55, "Description for product 55", "Product 55", 793m },
                    { 54, "Description for product 54", "Product 54", 875m },
                    { 53, "Description for product 53", "Product 53", 937m },
                    { 74, "Description for product 74", "Product 74", 138m },
                    { 52, "Description for product 52", "Product 52", 896m },
                    { 75, "Description for product 75", "Product 75", 724m },
                    { 77, "Description for product 77", "Product 77", 149m },
                    { 98, "Description for product 98", "Product 98", 446m },
                    { 97, "Description for product 97", "Product 97", 561m },
                    { 96, "Description for product 96", "Product 96", 499m },
                    { 95, "Description for product 95", "Product 95", 933m },
                    { 94, "Description for product 94", "Product 94", 511m },
                    { 93, "Description for product 93", "Product 93", 776m },
                    { 92, "Description for product 92", "Product 92", 411m },
                    { 91, "Description for product 91", "Product 91", 882m },
                    { 90, "Description for product 90", "Product 90", 799m },
                    { 89, "Description for product 89", "Product 89", 631m },
                    { 88, "Description for product 88", "Product 88", 987m },
                    { 87, "Description for product 87", "Product 87", 290m },
                    { 86, "Description for product 86", "Product 86", 824m },
                    { 85, "Description for product 85", "Product 85", 988m },
                    { 84, "Description for product 84", "Product 84", 475m },
                    { 83, "Description for product 83", "Product 83", 917m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 82, "Description for product 82", "Product 82", 616m },
                    { 81, "Description for product 81", "Product 81", 191m },
                    { 80, "Description for product 80", "Product 80", 880m },
                    { 79, "Description for product 79", "Product 79", 824m },
                    { 78, "Description for product 78", "Product 78", 886m },
                    { 76, "Description for product 76", "Product 76", 623m },
                    { 51, "Description for product 51", "Product 51", 657m },
                    { 50, "Description for product 50", "Product 50", 522m },
                    { 49, "Description for product 49", "Product 49", 558m },
                    { 22, "Description for product 22", "Product 22", 791m },
                    { 21, "Description for product 21", "Product 21", 523m },
                    { 20, "Description for product 20", "Product 20", 252m },
                    { 19, "Description for product 19", "Product 19", 379m },
                    { 18, "Description for product 18", "Product 18", 136m },
                    { 17, "Description for product 17", "Product 17", 884m },
                    { 16, "Description for product 16", "Product 16", 972m },
                    { 15, "Description for product 15", "Product 15", 380m },
                    { 14, "Description for product 14", "Product 14", 161m },
                    { 13, "Description for product 13", "Product 13", 917m },
                    { 12, "Description for product 12", "Product 12", 981m },
                    { 11, "Description for product 11", "Product 11", 977m },
                    { 10, "Description for product 10", "Product 10", 957m },
                    { 9, "Description for product 9", "Product 9", 941m },
                    { 8, "Description for product 8", "Product 8", 335m },
                    { 7, "Description for product 7", "Product 7", 504m },
                    { 6, "Description for product 6", "Product 6", 334m },
                    { 5, "Description for product 5", "Product 5", 900m },
                    { 4, "Description for product 4", "Product 4", 488m },
                    { 3, "Description for product 3", "Product 3", 523m },
                    { 2, "Description for product 2", "Product 2", 574m },
                    { 23, "Description for product 23", "Product 23", 649m },
                    { 24, "Description for product 24", "Product 24", 890m },
                    { 25, "Description for product 25", "Product 25", 718m },
                    { 26, "Description for product 26", "Product 26", 556m },
                    { 48, "Description for product 48", "Product 48", 411m },
                    { 47, "Description for product 47", "Product 47", 465m },
                    { 46, "Description for product 46", "Product 46", 838m },
                    { 45, "Description for product 45", "Product 45", 166m },
                    { 44, "Description for product 44", "Product 44", 762m },
                    { 43, "Description for product 43", "Product 43", 742m },
                    { 42, "Description for product 42", "Product 42", 899m },
                    { 41, "Description for product 41", "Product 41", 527m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 40, "Description for product 40", "Product 40", 340m },
                    { 39, "Description for product 39", "Product 39", 855m },
                    { 99, "Description for product 99", "Product 99", 525m },
                    { 38, "Description for product 38", "Product 38", 545m },
                    { 36, "Description for product 36", "Product 36", 806m },
                    { 35, "Description for product 35", "Product 35", 196m },
                    { 34, "Description for product 34", "Product 34", 424m },
                    { 33, "Description for product 33", "Product 33", 863m },
                    { 32, "Description for product 32", "Product 32", 227m },
                    { 31, "Description for product 31", "Product 31", 720m },
                    { 30, "Description for product 30", "Product 30", 645m },
                    { 29, "Description for product 29", "Product 29", 897m },
                    { 28, "Description for product 28", "Product 28", 476m },
                    { 27, "Description for product 27", "Product 27", 928m },
                    { 37, "Description for product 37", "Product 37", 944m },
                    { 100, "Description for product 100", "Product 100", 317m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 31 },
                    { 73, 73, 7 },
                    { 72, 72, 66 },
                    { 71, 71, 8 },
                    { 70, 70, 82 },
                    { 69, 69, 24 },
                    { 68, 68, 58 },
                    { 67, 67, 33 },
                    { 66, 66, 61 },
                    { 65, 65, 66 },
                    { 64, 64, 88 },
                    { 63, 63, 66 },
                    { 62, 62, 85 },
                    { 61, 61, 52 },
                    { 60, 60, 36 },
                    { 59, 59, 66 },
                    { 58, 58, 73 },
                    { 57, 57, 86 },
                    { 56, 56, 66 },
                    { 55, 55, 39 },
                    { 54, 54, 90 },
                    { 53, 53, 97 },
                    { 74, 74, 25 },
                    { 52, 52, 38 },
                    { 75, 75, 73 },
                    { 77, 77, 64 },
                    { 98, 98, 12 },
                    { 97, 97, 76 },
                    { 96, 96, 6 },
                    { 95, 95, 33 },
                    { 94, 94, 76 },
                    { 93, 93, 0 },
                    { 92, 92, 66 },
                    { 91, 91, 60 },
                    { 90, 90, 85 },
                    { 89, 89, 80 },
                    { 88, 88, 33 },
                    { 87, 87, 82 },
                    { 86, 86, 79 },
                    { 85, 85, 56 },
                    { 84, 84, 81 },
                    { 83, 83, 21 }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 82, 82, 90 },
                    { 81, 81, 64 },
                    { 80, 80, 24 },
                    { 79, 79, 28 },
                    { 78, 78, 50 },
                    { 76, 76, 44 },
                    { 51, 51, 65 },
                    { 50, 50, 11 },
                    { 49, 49, 62 },
                    { 22, 22, 64 },
                    { 21, 21, 87 },
                    { 20, 20, 65 },
                    { 19, 19, 12 },
                    { 18, 18, 20 },
                    { 17, 17, 33 },
                    { 16, 16, 49 },
                    { 15, 15, 29 },
                    { 14, 14, 77 },
                    { 13, 13, 78 },
                    { 12, 12, 57 },
                    { 11, 11, 42 },
                    { 10, 10, 84 },
                    { 9, 9, 9 },
                    { 8, 8, 59 },
                    { 7, 7, 70 },
                    { 6, 6, 26 },
                    { 5, 5, 3 },
                    { 4, 4, 58 },
                    { 3, 3, 56 },
                    { 2, 2, 67 },
                    { 23, 23, 80 },
                    { 24, 24, 83 },
                    { 25, 25, 91 },
                    { 26, 26, 66 },
                    { 48, 48, 82 },
                    { 47, 47, 2 },
                    { 46, 46, 67 },
                    { 45, 45, 54 },
                    { 44, 44, 29 },
                    { 43, 43, 0 },
                    { 42, 42, 42 },
                    { 41, 41, 91 }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 40, 40, 86 },
                    { 39, 39, 13 },
                    { 99, 99, 62 },
                    { 38, 38, 67 },
                    { 36, 36, 47 },
                    { 35, 35, 52 },
                    { 34, 34, 19 },
                    { 33, 33, 23 },
                    { 32, 32, 80 },
                    { 31, 31, 17 },
                    { 30, 30, 21 },
                    { 29, 29, 93 },
                    { 28, 28, 84 },
                    { 27, 27, 73 },
                    { 37, 37, 40 },
                    { 100, 100, 92 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                schema: "Catalog",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
