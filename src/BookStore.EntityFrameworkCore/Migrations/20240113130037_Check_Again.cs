using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class CheckAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTranslated",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BookType",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookType",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsTranslated",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
