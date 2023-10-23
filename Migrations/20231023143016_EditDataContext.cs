using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Inventory_System.Migrations
{
    /// <inheritdoc />
    public partial class EditDataContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shelve_ShelveId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelve",
                table: "Shelve");

            migrationBuilder.RenameTable(
                name: "Shelve",
                newName: "Shelves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shelves_ShelveId",
                table: "Books",
                column: "ShelveId",
                principalTable: "Shelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shelves_ShelveId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelves",
                table: "Shelves");

            migrationBuilder.RenameTable(
                name: "Shelves",
                newName: "Shelve");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelve",
                table: "Shelve",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shelve_ShelveId",
                table: "Books",
                column: "ShelveId",
                principalTable: "Shelve",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
