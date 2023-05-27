using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPrism.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_OrderItems_Id",
                table: "ProductItems");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderItemId",
                table: "ProductItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_OrderItemId",
                table: "ProductItems",
                column: "OrderItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_OrderItems_OrderItemId",
                table: "ProductItems",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_OrderItems_OrderItemId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_OrderItemId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "ProductItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_OrderItems_Id",
                table: "ProductItems",
                column: "Id",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
