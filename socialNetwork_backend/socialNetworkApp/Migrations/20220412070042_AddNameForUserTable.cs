using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socialNetworkApp.Migrations
{
    public partial class AddNameForUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "surname",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true,
                defaultValue: "");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "surname",
                schema: "public",
                table: "users");
        }
    }
}
