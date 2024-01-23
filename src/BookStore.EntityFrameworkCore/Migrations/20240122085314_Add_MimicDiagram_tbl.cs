using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddMimicDiagramtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile");

            migrationBuilder.DropColumn(
                name: "RecordUserId",
                table: "tbl_EM_MimicProfile");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_EM_MimicProfile",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "tbl_EM_MimicDiagram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MimicDiagramName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimicDiagramAuthorization = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EM_MimicDiagram", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EM_MimicDiagram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_EM_MimicProfile");

            migrationBuilder.AddColumn<string>(
                name: "RecordUserId",
                table: "tbl_EM_MimicProfile",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile",
                column: "RecordUserId");
        }
    }
}
