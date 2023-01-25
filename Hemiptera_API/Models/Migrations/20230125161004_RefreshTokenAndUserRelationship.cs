using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HemipteraAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenAndUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<Guid>(
                name: "RefreshTokenId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RefreshTokenId",
                table: "AspNetUsers",
                column: "RefreshTokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RefreshTokens_RefreshTokenId",
                table: "AspNetUsers",
                column: "RefreshTokenId",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RefreshTokens_RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
