using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayPrism.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedGiveawaysEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Giveaways",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giveaways", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Giveaways_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Giveaways_UserProfiles_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GiveawayUserProfile",
                columns: table => new
                {
                    GiveawaysId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiveawayUserProfile", x => new { x.GiveawaysId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_GiveawayUserProfile_Giveaways_GiveawaysId",
                        column: x => x.GiveawaysId,
                        principalTable: "Giveaways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiveawayUserProfile_UserProfiles_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Giveaways_ProductId",
                table: "Giveaways",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Giveaways_WinnerId",
                table: "Giveaways",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GiveawayUserProfile_ParticipantsId",
                table: "GiveawayUserProfile",
                column: "ParticipantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiveawayUserProfile");

            migrationBuilder.DropTable(
                name: "Giveaways");
        }
    }
}
