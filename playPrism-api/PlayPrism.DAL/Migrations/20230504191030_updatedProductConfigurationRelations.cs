using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPrism.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedProductConfigurationRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfigurations_Products_ProductId",
                table: "ProductConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfigurations_VariationOptions_VariationOptionId",
                table: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_ProductConfigurations_ProductId",
                table: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_ProductConfigurations_VariationOptionId",
                table: "ProductConfigurations");

            migrationBuilder.DropColumn(
                name: "Values",
                table: "VariationOptions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductConfigurations");

            migrationBuilder.DropColumn(
                name: "VariationOptionId",
                table: "ProductConfigurations");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductConfigurationId",
                table: "VariationOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "VariationOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "VariationOptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "ProductConfigurations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariationOptions_ProductConfigurationId",
                table: "VariationOptions",
                column: "ProductConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationOptions_ProductId",
                table: "VariationOptions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductConfigurations_CategoryId",
                table: "ProductConfigurations",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfigurations_ProductCategories_CategoryId",
                table: "ProductConfigurations",
                column: "CategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariationOptions_ProductConfigurations_ProductConfiguration~",
                table: "VariationOptions",
                column: "ProductConfigurationId",
                principalTable: "ProductConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariationOptions_Products_ProductId",
                table: "VariationOptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfigurations_ProductCategories_CategoryId",
                table: "ProductConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_VariationOptions_ProductConfigurations_ProductConfiguration~",
                table: "VariationOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_VariationOptions_Products_ProductId",
                table: "VariationOptions");

            migrationBuilder.DropIndex(
                name: "IX_VariationOptions_ProductConfigurationId",
                table: "VariationOptions");

            migrationBuilder.DropIndex(
                name: "IX_VariationOptions_ProductId",
                table: "VariationOptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductConfigurations_CategoryId",
                table: "ProductConfigurations");

            migrationBuilder.DropColumn(
                name: "ProductConfigurationId",
                table: "VariationOptions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "VariationOptions");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "VariationOptions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductConfigurations");

            migrationBuilder.AddColumn<string[]>(
                name: "Values",
                table: "VariationOptions",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductConfigurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VariationOptionId",
                table: "ProductConfigurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductConfigurations_ProductId",
                table: "ProductConfigurations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductConfigurations_VariationOptionId",
                table: "ProductConfigurations",
                column: "VariationOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfigurations_Products_ProductId",
                table: "ProductConfigurations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfigurations_VariationOptions_VariationOptionId",
                table: "ProductConfigurations",
                column: "VariationOptionId",
                principalTable: "VariationOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
