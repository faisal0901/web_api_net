using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokonyadiaRestAPI.Migrations
{
    /// <inheritdoc />
    public partial class initcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "m_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_role", x => x.id);
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
                name: "m_user_credential",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    roleid = table.Column<Guid>(name: "role_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_user_credential", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_user_credential_m_role_role_id",
                        column: x => x.roleid,
                        principalTable: "m_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "m_customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customername = table.Column<string>(name: "customer_name", type: "NVarchar(48)", nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "NVarchar(14)", nullable: true),
                    address = table.Column<string>(type: "NVarchar(100)", nullable: true),
                    UserCredentialsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_customer_m_user_credential_UserCredentialsId",
                        column: x => x.UserCredentialsId,
                        principalTable: "m_user_credential",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_purchase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transdate = table.Column<DateTime>(name: "trans_date", type: "datetime2", nullable: false),
                    customerid = table.Column<Guid>(name: "customer_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_purchase", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_purchase_m_customer_customer_id",
                        column: x => x.customerid,
                        principalTable: "m_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_purchase_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    purchaseid = table.Column<Guid>(name: "purchase_id", type: "uniqueidentifier", nullable: true),
                    productpriceid = table.Column<Guid>(name: "product_price_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_purchase_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_purchase_detail_m_product_price_product_price_id",
                        column: x => x.productpriceid,
                        principalTable: "m_product_price",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_purchase_detail_t_purchase_purchase_id",
                        column: x => x.purchaseid,
                        principalTable: "t_purchase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_phone_number",
                table: "m_customer",
                column: "phone_number",
                unique: true,
                filter: "[phone_number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_UserCredentialsId",
                table: "m_customer",
                column: "UserCredentialsId");

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

            migrationBuilder.CreateIndex(
                name: "IX_m_user_credential_email",
                table: "m_user_credential",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_user_credential_role_id",
                table: "m_user_credential",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_customer_id",
                table: "t_purchase",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_detail_product_price_id",
                table: "t_purchase_detail",
                column: "product_price_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_detail_purchase_id",
                table: "t_purchase_detail",
                column: "purchase_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_purchase_detail");

            migrationBuilder.DropTable(
                name: "m_product_price");

            migrationBuilder.DropTable(
                name: "t_purchase");

            migrationBuilder.DropTable(
                name: "m_product");

            migrationBuilder.DropTable(
                name: "m_store");

            migrationBuilder.DropTable(
                name: "m_customer");

            migrationBuilder.DropTable(
                name: "m_user_credential");

            migrationBuilder.DropTable(
                name: "m_role");
        }
    }
}
