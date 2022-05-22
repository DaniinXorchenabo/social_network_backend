

#nullable disable

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using socialNetworkApp.api.controllers.chat;

namespace socialNetworkApp.Migrations
{
    public partial class CreateRelationsAndEnumFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ChatToUserRole[]>(
                name: "roles",
                schema: "public",
                table: "chats_to_users",
                type: "chat_to_user_role[]",
                nullable: false,
                defaultValue: new  List<string>{"user"});

            migrationBuilder.AddColumn<ChatCreatorType>(
                name: "chat_creator_type",
                schema: "public",
                table: "chats",
                type: "chat_creator_type",
                nullable: false,
                defaultValue: "user");

            migrationBuilder.AddColumn<ChatType>(
                name: "chat_type",
                schema: "public",
                table: "chats",
                type: "chat_type",
                nullable: false,
                defaultValue: "simple");

            migrationBuilder.CreateIndex(
                name: "IX_chats_to_users_chat_id",
                schema: "public",
                table: "chats_to_users",
                column: "chat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_chats_to_users_chats_chat_id",
                schema: "public",
                table: "chats_to_users",
                column: "chat_id",
                principalSchema: "public",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chats_to_users_users_user_id",
                schema: "public",
                table: "chats_to_users",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chats_to_users_chats_chat_id",
                schema: "public",
                table: "chats_to_users");

            migrationBuilder.DropForeignKey(
                name: "FK_chats_to_users_users_user_id",
                schema: "public",
                table: "chats_to_users");

            migrationBuilder.DropIndex(
                name: "IX_chats_to_users_chat_id",
                schema: "public",
                table: "chats_to_users");

            migrationBuilder.DropColumn(
                name: "roles",
                schema: "public",
                table: "chats_to_users");

            migrationBuilder.DropColumn(
                name: "chat_creator_type",
                schema: "public",
                table: "chats");

            migrationBuilder.DropColumn(
                name: "chat_type",
                schema: "public",
                table: "chats");
        }
    }
}
