using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApplication.Migrations
{
    public partial class prizeandbasketfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basket-products_baskets_BasketId",
                table: "basket-products");

            migrationBuilder.DropForeignKey(
                name: "FK_baskets_Users_UserId",
                table: "baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_baskets",
                table: "baskets");

            migrationBuilder.RenameTable(
                name: "baskets",
                newName: "Baskets");

            migrationBuilder.RenameIndex(
                name: "IX_baskets_UserId",
                table: "Baskets",
                newName: "IX_Baskets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Prizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageNameInFileSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prizes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_basket-products_Baskets_BasketId",
                table: "basket-products",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basket-products_Baskets_BasketId",
                table: "basket-products");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets");

            migrationBuilder.DropTable(
                name: "Prizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.RenameTable(
                name: "Baskets",
                newName: "baskets");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_UserId",
                table: "baskets",
                newName: "IX_baskets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_baskets",
                table: "baskets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_basket-products_baskets_BasketId",
                table: "basket-products",
                column: "BasketId",
                principalTable: "baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_baskets_Users_UserId",
                table: "baskets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
