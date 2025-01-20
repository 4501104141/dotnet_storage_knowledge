using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class _118 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "tb_teacher",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "tb_student",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "tb_state_class",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "tb_school",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "tb_class",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "tb_teacher");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "tb_state_class");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "tb_school");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "tb_class");
        }
    }
}
