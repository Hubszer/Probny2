using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proba2.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pastry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pastry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FullfilledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_pastry",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PastryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_pastry", x => new { x.OrderId, x.PastryId });
                    table.ForeignKey(
                        name: "FK_order_pastry_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_pastry_pastry_PastryId",
                        column: x => x.PastryId,
                        principalTable: "pastry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "client",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1, "John", "Doe" });

            migrationBuilder.InsertData(
                table: "employee",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Adam", "Nowak" },
                    { 2, "Aleksandra", "Wiśniewska" }
                });

            migrationBuilder.InsertData(
                table: "pastry",
                columns: new[] { "Id", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Drożdzówka", 3.3m, "A" },
                    { 2, "Babka cytrynowa", 21.23m, "B" },
                    { 3, "Jagodzianka", 7.2m, "A" }
                });

            migrationBuilder.InsertData(
                table: "order",
                columns: new[] { "Id", "AcceptedAt", "ClientId", "Comments", "EmployeeId", "FullfilledAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lorem ipsum ...", 2, new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lorem ipsum ...", 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "order_pastry",
                columns: new[] { "OrderId", "PastryId", "Amount", "Comment" },
                values: new object[,]
                {
                    { 1, 1, 3, null },
                    { 1, 3, 4, "Lorem ipsum ..." },
                    { 2, 1, 12, null },
                    { 2, 2, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_ClientId",
                table: "order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_order_EmployeeId",
                table: "order",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_order_pastry_PastryId",
                table: "order_pastry",
                column: "PastryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_pastry");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "pastry");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}
