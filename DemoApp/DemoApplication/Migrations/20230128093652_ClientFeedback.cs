using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApplication.Migrations
{
    public partial class ClientFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserActivations_UserId",
                table: "UserActivations");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ClientFeedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageNameInFileSystem",
                table: "ClientFeedbacks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivations_UserId",
                table: "UserActivations",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserActivations_UserId",
                table: "UserActivations");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ClientFeedbacks");

            migrationBuilder.DropColumn(
                name: "ImageNameInFileSystem",
                table: "ClientFeedbacks");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivations_UserId",
                table: "UserActivations",
                column: "UserId");
        }
    }
}
