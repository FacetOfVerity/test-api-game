using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleRoom.Infrastructure.Migrations
{
    public partial class UpdatePlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHost",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Players_NickName",
                table: "Players",
                column: "NickName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_NickName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsHost",
                table: "Games");
        }
    }
}
