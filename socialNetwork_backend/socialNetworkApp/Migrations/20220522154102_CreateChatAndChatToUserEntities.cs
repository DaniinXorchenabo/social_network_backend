using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socialNetworkApp.Migrations
{
    public partial class CreateChatAndChatToUserEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .Annotation("Npgsql:Enum:chat_creator_type", "user,group")
                .Annotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .Annotation("Npgsql:Enum:chat_type", "simple,secret,fantom")
                .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "public",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "chats",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true),
                    invitation_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chats_to_users",
                schema: "public",
                columns: table => new
                {
                    chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats_to_users", x => new { x.user_id, x.chat_id });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chats",
                schema: "public");

            migrationBuilder.DropTable(
                name: "chats_to_users",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "public",
                table: "users");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .OldAnnotation("Npgsql:Enum:chat_creator_type", "user,group")
                .OldAnnotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .OldAnnotation("Npgsql:Enum:chat_type", "simple,secret,fantom");
        }
    }
}
