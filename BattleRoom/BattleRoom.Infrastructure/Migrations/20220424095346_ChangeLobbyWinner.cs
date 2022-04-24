using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleRoom.Infrastructure.Migrations
{
    public partial class ChangeLobbyWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Lobbies");

            migrationBuilder.AddColumn<bool>(
                name: "IsWinner",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWinner",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "WinnerId",
                table: "Lobbies",
                type: "uuid",
                nullable: true);
        }
    }
}
