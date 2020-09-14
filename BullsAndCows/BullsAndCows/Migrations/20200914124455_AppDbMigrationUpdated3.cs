using Microsoft.EntityFrameworkCore.Migrations;

namespace BullsAndCows.Migrations
{
    public partial class AppDbMigrationUpdated3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameId",
                table: "UserTurns",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "UserTurns");
        }
    }
}
