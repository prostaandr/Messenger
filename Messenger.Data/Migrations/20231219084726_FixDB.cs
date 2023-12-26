using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messenger.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMessages_Users_AddresserId",
                table: "GroupMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_Users_AddresseeId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_Users_AddresserId",
                table: "UserMessages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "AddresserId",
                table: "UserMessages",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "AddresseeId",
                table: "UserMessages",
                newName: "ReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessages_AddresserId",
                table: "UserMessages",
                newName: "IX_UserMessages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessages_AddresseeId",
                table: "UserMessages",
                newName: "IX_UserMessages_ReciverId");

            migrationBuilder.RenameColumn(
                name: "AddresserId",
                table: "GroupMessages",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMessages_AddresserId",
                table: "GroupMessages",
                newName: "IX_GroupMessages_SenderId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastOnlineDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMessages_Users_SenderId",
                table: "GroupMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_Users_ReciverId",
                table: "UserMessages",
                column: "ReciverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_Users_SenderId",
                table: "UserMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMessages_Users_SenderId",
                table: "GroupMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_Users_ReciverId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_Users_SenderId",
                table: "UserMessages");

            migrationBuilder.DropColumn(
                name: "LastOnlineDate",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "UserMessages",
                newName: "AddresserId");

            migrationBuilder.RenameColumn(
                name: "ReciverId",
                table: "UserMessages",
                newName: "AddresseeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessages_SenderId",
                table: "UserMessages",
                newName: "IX_UserMessages_AddresserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessages_ReciverId",
                table: "UserMessages",
                newName: "IX_UserMessages_AddresseeId");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "GroupMessages",
                newName: "AddresserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMessages_SenderId",
                table: "GroupMessages",
                newName: "IX_GroupMessages_AddresserId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMessages_Users_AddresserId",
                table: "GroupMessages",
                column: "AddresserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_Users_AddresseeId",
                table: "UserMessages",
                column: "AddresseeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_Users_AddresserId",
                table: "UserMessages",
                column: "AddresserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
