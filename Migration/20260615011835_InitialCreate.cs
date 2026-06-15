using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspLab04.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaleId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kỹ năng sống" },
                    { 2, "Tiểu thuyết" },
                    { 3, "Khoa học" },
                    { 4, "Công nghệ" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "BookCode", "GenreId", "Price", "Publisher", "Quantity", "Title" },
                values: new object[,]
                {
                    { 1, "Dale Carnegie", "BK-SKL-001", 1, 88000m, "NXB Tổng hợp TP.HCM", 25, "Đắc Nhân Tâm" },
                    { 2, "Daniel Kahneman", "BK-SKL-002", 1, 120000m, "NXB Lao Động", 3, "Tư Duy Nhanh Và Chậm" },
                    { 3, "Paulo Coelho", "BK-NOV-001", 2, 75000m, "NXB Hội Nhà Văn", 10, "Nhà Giả Kim" },
                    { 4, "Yuval Noah Harari", "BK-NOV-002", 2, 175000m, "NXB Tri Thức", 2, "Sapiens: Lược Sử Loài Người" },
                    { 5, "Stephen Hawking", "BK-SCI-001", 3, 120000m, "NXB Trẻ", 0, "Lược Sử Thời Gian" },
                    { 6, "Robert C. Martin", "BK-TECH-001", 4, 220000m, "NXB Lao Động", 8, "Clean Code" },
                    { 7, "David Thomas", "BK-TECH-002", 4, 195000m, "NXB Lao Động", 4, "The Pragmatic Programmer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_BookId",
                table: "SaleItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems",
                column: "SaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}