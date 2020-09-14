using Microsoft.EntityFrameworkCore.Migrations;

namespace BullsAndCows.Migrations
{
    public partial class AppDbMigrationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTurns",
                table: "UserTurns");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserTurns",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserTurns",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTurns",
                table: "UserTurns",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTurns",
                table: "UserTurns");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTurns");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserTurns",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTurns",
                table: "UserTurns",
                column: "UserName");
        }
    }
}
