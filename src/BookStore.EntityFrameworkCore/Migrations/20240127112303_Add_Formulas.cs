using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddFormulas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_EM_Formulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    FormulaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormulaType = table.Column<int>(type: "int", nullable: false),
                    FormulaMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormulaInputMinimum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormulaInputMaximum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormulaOutputMinimum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormulaOutputMaximim = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EM_Formulas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EM_Formulas");
        }
    }
}
