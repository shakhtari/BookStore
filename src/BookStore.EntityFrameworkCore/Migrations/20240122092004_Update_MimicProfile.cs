using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMimicProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "tbl_EM_MimicProfile",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "tbl_EM_MimicProfile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_EM_MimicProfile",
                table: "tbl_EM_MimicProfile",
                column: "Id");
        }
    }
}
