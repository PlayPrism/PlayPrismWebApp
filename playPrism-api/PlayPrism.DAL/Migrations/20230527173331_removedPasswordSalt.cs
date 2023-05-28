using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPrism.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removedPasswordSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "UserProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "UserProfiles",
                type: "text",
                nullable: true);
        }
    }
}
