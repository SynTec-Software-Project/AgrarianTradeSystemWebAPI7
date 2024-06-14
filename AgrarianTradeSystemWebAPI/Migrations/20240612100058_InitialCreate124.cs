using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrarianTradeSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromCourierID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    CourierEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrdersOrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_Couriers_CourierEmail",
                        column: x => x.CourierEmail,
                        principalTable: "Couriers",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_Notification_Orders_OrdersOrderID",
                        column: x => x.OrdersOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_Notification_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_Notification_Users_BuyerEmail",
                        column: x => x.BuyerEmail,
                        principalTable: "Users",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_BuyerEmail",
                table: "Notification",
                column: "BuyerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CourierEmail",
                table: "Notification",
                column: "CourierEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OrdersOrderID",
                table: "Notification",
                column: "OrdersOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ProductID",
                table: "Notification",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
