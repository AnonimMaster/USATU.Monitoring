using Microsoft.EntityFrameworkCore.Migrations;

namespace USATU.Monitoring.Data.Migrations
{
    public partial class AddTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "user",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<string>(type: "text", nullable: true),
                    result = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "Users",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
