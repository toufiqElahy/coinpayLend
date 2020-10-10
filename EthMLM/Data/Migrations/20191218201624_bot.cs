using Microsoft.EntityFrameworkCore.Migrations;

namespace EthMLM.Data.Migrations
{
    public partial class bot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EthAddress",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Passwrd",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EthAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Passwrd",
                table: "AspNetUsers");
        }
    }
}
