using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socialNetworkApp.Migrations
{
    public partial class AddUnigueForSomeParams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(" DELETE FROM users WHERE users.email = ANY ( ( SELECT ee.email FROM ( SELECT Count(users.id) as count_row, users.email FROM users GROUP BY email ) AS ee WHERE ee.count_row > 1 ) ) ;");
            migrationBuilder.Sql(" DELETE FROM users WHERE users.username = ANY ( ( SELECT ee.username FROM ( SELECT Count(users.id) as count_row, users.username FROM users GROUP BY username ) AS ee WHERE ee.count_row > 1 ) ) ;");
            migrationBuilder.CreateIndex(
                name: "Index__UniqueEmail",
                schema: "public",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index__UniqueUsername",
                schema: "public",
                table: "users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index__UniqueEmail",
                schema: "public",
                table: "users");

            migrationBuilder.DropIndex(
                name: "Index__UniqueUsername",
                schema: "public",
                table: "users");
        }
    }
}
