using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokonyadiaRestAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customername = table.Column<string>(name: "customer_name", type: "NVarchar(48)", nullable: false),
                    phonenumber = table.Column<string>(name: "phone_number", type: "NVarchar(14)", nullable: false),
                    address = table.Column<string>(type: "NVarchar(100)", nullable: false),
                    email = table.Column<string>(type: "NVarchar(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productname = table.Column<string>(name: "product_name", type: "NVarchar(50)", nullable: false),
                    description = table.Column<string>(type: "NVarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_store",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    storename = table.Column<string>(name: "store_name", type: "NVarchar(48)", nullable: false),
                    address = table.Column<string>(type: "NVarchar(100)", nullable: false),
                    phonenumber = table.Column<string>(name: "phone_number", type: "NVarchar(14)", nullable: false),
                    siupnumber = table.Column<string>(name: "siup_number", type: "NVarchar(9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_store", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_product_price",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    productid = table.Column<Guid>(name: "product_id", type: "uniqueidentifier", nullable: false),
                    storeid = table.Column<Guid>(name: "store_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product_price", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_product_price_m_product_product_id",
                        column: x => x.productid,
                        principalTable: "m_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_product_price_m_store_store_id",
                        column: x => x.storeid,
                        principalTable: "m_store",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_email",
                table: "m_customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_phone_number",
                table: "m_customer",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_product_price_product_id",
                table: "m_product_price",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_product_price_store_id",
                table: "m_product_price",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_store_phone_number",
                table: "m_store",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_store_siup_number",
                table: "m_store",
                column: "siup_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_customer");

            migrationBuilder.DropTable(
                name: "m_product_price");

            migrationBuilder.DropTable(
                name: "m_product");

            migrationBuilder.DropTable(
                name: "m_store");
        }
    }
}
