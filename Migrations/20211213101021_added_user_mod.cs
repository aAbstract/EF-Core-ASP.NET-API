using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace db_api_test.Migrations
{
    public partial class added_user_mod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "Users",
                newName: "Userrole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Userrole",
                table: "Users",
                newName: "UserRole");
        }
    }
}
