using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using socialNetworkApp.api.controllers.chat;
using socialNetworkApp.api.controllers.messages;

#nullable disable

namespace socialNetworkApp.Migrations
{
    public partial class RenameEnumsValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .Annotation("Npgsql:Enum:chat_creator_type_enum", "user,group")
                .Annotation("Npgsql:Enum:chat_to_user_role_enum", "user,admin,creator,black_list")
                .Annotation("Npgsql:Enum:chat_type_enum", "simple,secret,fantom")
                .Annotation("Npgsql:Enum:message_type_enum", "text,system_message")
                .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .OldAnnotation("Npgsql:Enum:chat_creator_type", "user,group")
                .OldAnnotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .OldAnnotation("Npgsql:Enum:chat_type", "simple,secret,fantom")
                .OldAnnotation("Npgsql:Enum:message_type", "text,system_massage");

            migrationBuilder.AlterColumn<MessageTypeEnum>(
                name: "message_type",
                schema: "public",
                table: "messages",
                type: "message_type_enum",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "message_type");

            migrationBuilder.AlterColumn<List<ChatToUserRoleEnum>>(
                name: "roles",
                schema: "public",
                table: "chats_to_users",
                type: "chat_to_user_role_enum[]",
                nullable: false,
                oldClrType: typeof(int[]),
                oldType: "chat_to_user_role[]");

            migrationBuilder.AlterColumn<ChatTypeEnum>(
                name: "chat_type",
                schema: "public",
                table: "chats",
                type: "chat_type_enum",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "chat_type");

            migrationBuilder.AlterColumn<ChatCreatorTypeEnum>(
                name: "chat_creator_type",
                schema: "public",
                table: "chats",
                type: "chat_creator_type_enum",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "chat_creator_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .Annotation("Npgsql:Enum:chat_creator_type", "user,group")
                .Annotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .Annotation("Npgsql:Enum:chat_type", "simple,secret,fantom")
                .Annotation("Npgsql:Enum:message_type", "text,system_massage")
                .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .OldAnnotation("Npgsql:Enum:chat_creator_type_enum", "user,group")
                .OldAnnotation("Npgsql:Enum:chat_to_user_role_enum", "user,admin,creator,black_list")
                .OldAnnotation("Npgsql:Enum:chat_type_enum", "simple,secret,fantom")
                .OldAnnotation("Npgsql:Enum:message_type_enum", "text,system_message");

            migrationBuilder.AlterColumn<int>(
                name: "message_type",
                schema: "public",
                table: "messages",
                type: "message_type",
                nullable: false,
                oldClrType: typeof(MessageTypeEnum),
                oldType: "message_type_enum");

            migrationBuilder.AlterColumn<int[]>(
                name: "roles",
                schema: "public",
                table: "chats_to_users",
                type: "chat_to_user_role[]",
                nullable: false,
                oldClrType: typeof(List<ChatToUserRoleEnum>),
                oldType: "chat_to_user_role_enum[]");

            migrationBuilder.AlterColumn<int>(
                name: "chat_type",
                schema: "public",
                table: "chats",
                type: "chat_type",
                nullable: false,
                oldClrType: typeof(ChatTypeEnum),
                oldType: "chat_type_enum");

            migrationBuilder.AlterColumn<int>(
                name: "chat_creator_type",
                schema: "public",
                table: "chats",
                type: "chat_creator_type",
                nullable: false,
                oldClrType: typeof(ChatCreatorTypeEnum),
                oldType: "chat_creator_type_enum");
        }
    }
}
