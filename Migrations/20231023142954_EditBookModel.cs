using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_Inventory_System.Migrations
{
    /// <inheritdoc />
    public partial class EditBookModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelveId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shelve",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShelveName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelve", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ShelveId",
                table: "Books",
                column: "ShelveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shelve_ShelveId",
                table: "Books",
                column: "ShelveId",
                principalTable: "Shelve",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shelve_ShelveId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Shelve");

            migrationBuilder.DropIndex(
                name: "IX_Books_ShelveId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ShelveId",
                table: "Books");
        }
    }
}
