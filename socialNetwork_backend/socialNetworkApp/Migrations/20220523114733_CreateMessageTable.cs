using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socialNetworkApp.Migrations
{
    public partial class CreateMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .Annotation("Npgsql:Enum:chat_creator_type", "user,group")
                .Annotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .Annotation("Npgsql:Enum:chat_type", "simple,secret,fantom")
                .Annotation("Npgsql:Enum:message_type", "text,system_massage")
                .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .OldAnnotation("Npgsql:Enum:chat_creator_type", "user,group")
                .OldAnnotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .OldAnnotation("Npgsql:Enum:chat_type", "simple,secret,fantom");

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: true),
                    chat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_viewed = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_messages_chats_chat_id",
                        column: x => x.chat_id,
                        principalSchema: "public",
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_chats_to_users_author_id_chat_id",
                        columns: x => new { x.author_id, x.chat_id },
                        principalSchema: "public",
                        principalTable: "chats_to_users",
                        principalColumns: new[] { "user_id", "chat_id" });
                    table.ForeignKey(
                        name: "FK_messages_users_author_id",
                        column: x => x.author_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages_author_id_chat_id",
                schema: "public",
                table: "messages",
                columns: new[] { "author_id", "chat_id" });

            migrationBuilder.CreateIndex(
                name: "IX_messages_chat_id",
                schema: "public",
                table: "messages",
                column: "chat_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages",
                schema: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .Annotation("Npgsql:Enum:chat_creator_type", "user,group")
                .Annotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .Annotation("Npgsql:Enum:chat_type", "simple,secret,fantom")
                .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter")
                .OldAnnotation("Npgsql:Enum:chat_creator_type", "user,group")
                .OldAnnotation("Npgsql:Enum:chat_to_user_role", "user,admin,creator,black_list")
                .OldAnnotation("Npgsql:Enum:chat_type", "simple,secret,fantom")
                .OldAnnotation("Npgsql:Enum:message_type", "text,system_massage");
        }
    }
}
