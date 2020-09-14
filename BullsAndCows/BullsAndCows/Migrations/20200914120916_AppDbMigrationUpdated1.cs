using Microsoft.EntityFrameworkCore.Migrations;

namespace BullsAndCows.Migrations
{
    public partial class AppDbMigrationUpdated1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTurns",
                table: "UserTurns",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTurns",
                table: "UserTurns");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserTurns",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserTurns",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTurns",
                table: "UserTurns",
                column: "Id");
        }
    }
}
