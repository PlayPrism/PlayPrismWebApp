using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPrism.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductConfigurationRelationKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfigurations_ProductCategories_CategoryId",
                table: "ProductConfigurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "ProductConfigurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfigurations_ProductCategories_CategoryId",
                table: "ProductConfigurations",
                column: "CategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfigurations_ProductCategories_CategoryId",
                table: "ProductConfigurations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "ProductConfigurations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfigurations_ProductCategories_CategoryId",
                table: "ProductConfigurations",
                column: "CategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");
        }
    }
}
