using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class _115 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "classsID",
                table: "tb_student",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tb_class",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_class", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_student_classsID",
                table: "tb_student",
                column: "classsID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_student_tb_class_classsID",
                table: "tb_student",
                column: "classsID",
                principalTable: "tb_class",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_student_tb_class_classsID",
                table: "tb_student");

            migrationBuilder.DropTable(
                name: "tb_class");

            migrationBuilder.DropIndex(
                name: "IX_tb_student_classsID",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "classsID",
                table: "tb_student");
        }
    }
}
