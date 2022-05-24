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
            migrationBuilder.AlterColumn<string>(
                name: "message_type",
                schema: "public",
                table: "messages",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "message_type");

            migrationBuilder.AlterColumn<string[]>(
                name: "roles",
                schema: "public",
                table: "chats_to_users",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(int[]),
                oldType: "chat_to_user_role[]");

            migrationBuilder.AlterColumn<string>(
                name: "chat_type",
                schema: "public",
                table: "chats",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "chat_type");

            migrationBuilder.AlterColumn<string>(
                name: "chat_creator_type",
                schema: "public",
                table: "chats",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "chat_creator_type");

            migrationBuilder.RenameColumn(
                "chat_creator_type",
                "chats", 
                "old_chat_creator_type", 
                "public"
            );
            migrationBuilder.RenameColumn(
                "roles",
                "chats_to_users", 
                "old_roles", 
                "public"
            );
            migrationBuilder.RenameColumn(
                "chat_type",
                "chats", 
                "old_chat_type", 
                "public"
            );
            migrationBuilder.RenameColumn(
                "message_type",
                "messages", 
                "old_message_type", 
                "public"
            );

            
            migrationBuilder.AlterDatabase()
                // .Annotation("Npgsql:Enum:all_mods_enum",
                    // "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .Annotation("Npgsql:Enum:chat_creator_type_enum", "user,group")
                .Annotation("Npgsql:Enum:chat_to_user_role_enum", "user,admin,creator,black_list")
                .Annotation("Npgsql:Enum:chat_type_enum", "simple,secret,fantom")
                .Annotation("Npgsql:Enum:message_type_enum", "text,system_message");
                
            migrationBuilder.AddColumn<MessageTypeEnum>(
                name: "message_type",
                schema: "public",
                table: "messages",
                type: "message_type_enum",
                nullable: false,
                defaultValue: MessageTypeEnum.Text
            );

            migrationBuilder.AddColumn<List<ChatToUserRoleEnum>>(
                name: "roles",
                schema: "public",
                table: "chats_to_users",
                type: "chat_to_user_role_enum[]",
                nullable: false,
                defaultValue: new List<ChatToUserRoleEnum> {ChatToUserRoleEnum.User}
                );

            migrationBuilder.AddColumn<ChatTypeEnum>(
                name: "chat_type",
                schema: "public",
                table: "chats",
                type: "chat_type_enum",
                nullable: false,
                defaultValue:ChatTypeEnum.Simple
                );

            migrationBuilder.AddColumn<ChatCreatorTypeEnum>(
                    name: "chat_creator_type",
                    schema: "public",
                    table: "chats",
                    type: "chat_creator_type_enum",
                    nullable: false,
                    defaultValue: ChatCreatorTypeEnum.User
                    );

            migrationBuilder.Sql("UPDATE chats SET chat_creator_type = old_chat_creator_type::text::chat_creator_type_enum, chat_type = old_chat_type::text::chat_type_enum;");
            migrationBuilder.Sql("UPDATE chats_to_users SET roles = old_roles::text[]::chat_to_user_role_enum[];");
            migrationBuilder.Sql("UPDATE messages SET message_type = old_message_type::text::message_type_enum;");



            migrationBuilder.DropColumn("old_chat_creator_type", "chats");
            migrationBuilder.DropColumn("old_chat_type", "chats");
            migrationBuilder.DropColumn("old_roles", "chats_to_users");
            migrationBuilder.DropColumn("old_message_type", "messages");
            
            
            migrationBuilder.AlterDatabase()
                // .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .OldAnnotation("Npgsql:Enum:chat_creator_type", "user,group")
                .OldAnnotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .OldAnnotation("Npgsql:Enum:chat_type", "simple,secret,fantom")
                .OldAnnotation("Npgsql:Enum:message_type", "text,system_massage");


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
