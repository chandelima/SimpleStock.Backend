using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStock.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderItemFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Orders_SaleId",
                table: "OrdersItems");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "OrdersItems",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersItems_SaleId",
                table: "OrdersItems",
                newName: "IX_OrdersItems_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Orders_OrderId",
                table: "OrdersItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Orders_OrderId",
                table: "OrdersItems");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrdersItems",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersItems_OrderId",
                table: "OrdersItems",
                newName: "IX_OrdersItems_SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Orders_SaleId",
                table: "OrdersItems",
                column: "SaleId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
