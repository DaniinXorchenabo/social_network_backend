using Microsoft.EntityFrameworkCore.Migrations;
using socialNetworkApp.api.controllers.messages;

#nullable disable

namespace socialNetworkApp.Migrations
{
    public partial class AddMessageTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<MessageTypeEnum>(
                name: "message_type",
                schema: "public",
                table: "messages",
                type: "message_type",
                nullable: false,
                defaultValue: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message_type",
                schema: "public",
                table: "messages");
        }
    }
}
