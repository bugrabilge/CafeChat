using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class initialmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cafe_CafeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CafeId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CafeId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CafeId",
                table: "Users",
                column: "CafeId",
                unique: true,
                filter: "[CafeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cafe_CafeId",
                table: "Users",
                column: "CafeId",
                principalTable: "Cafe",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cafe_CafeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CafeId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CafeId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CafeId",
                table: "Users",
                column: "CafeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cafe_CafeId",
                table: "Users",
                column: "CafeId",
                principalTable: "Cafe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
