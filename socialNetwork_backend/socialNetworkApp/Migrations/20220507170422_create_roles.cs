using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using socialNetworkApp.api.controllers.modifiersOfAccess;

#nullable disable

namespace socialNetworkApp.Migrations
{
    public partial class create_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter");

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "public",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<AllModsEnum>>(
                name: "mods",
                schema: "public",
                table: "users",
                type: "all_mods_enum[]",
                nullable: false,
                defaultValue: "{}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "mods",
                schema: "public",
                table: "users");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:all_mods_enum", "msg_reader,msg_creator,msg_updater,msg_deleter,chat_reader,chat_creator,chat_updater,chat_deleter,user_reader,user_creator,user_updater,user_deleter,post_reader,post_creator,post_updater,post_deleter");
        }
    }
}
