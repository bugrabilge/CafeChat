using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updateCafeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cafe_CafeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CafeId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
