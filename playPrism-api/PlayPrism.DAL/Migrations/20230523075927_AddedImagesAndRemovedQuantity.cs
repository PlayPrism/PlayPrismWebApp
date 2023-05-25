using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPrism.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedImagesAndRemovedQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ProductItems_ProductItemId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductItemId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductItemId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderItems");

            migrationBuilder.AddColumn<string[]>(
                name: "Images",
                table: "Products",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductItemId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductItemId",
                table: "Orders",
                column: "ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ProductItems_ProductItemId",
                table: "Orders",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id");
        }
    }
}
