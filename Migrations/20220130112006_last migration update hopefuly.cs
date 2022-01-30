using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    public partial class lastmigrationupdatehopefuly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentings",
                table: "Rentings");

            migrationBuilder.DropIndex(
                name: "IX_Rentings_UserId",
                table: "Rentings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentings",
                table: "Rentings",
                columns: new[] { "UserId", "ProductsId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentings",
                table: "Rentings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentings",
                table: "Rentings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rentings_UserId",
                table: "Rentings",
                column: "UserId");
        }
    }
}
