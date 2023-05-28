using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPrism.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedPasswordSaltToUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "UserProfiles",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
