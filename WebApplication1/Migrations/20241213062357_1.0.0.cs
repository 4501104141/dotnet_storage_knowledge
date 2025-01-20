using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class _100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_school",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_school", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_student",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    schoolID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_student_tb_school_schoolID",
                        column: x => x.schoolID,
                        principalTable: "tb_school",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "tb_teacher",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    schoolID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_teacher", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_teacher_tb_school_schoolID",
                        column: x => x.schoolID,
                        principalTable: "tb_school",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_student_schoolID",
                table: "tb_student",
                column: "schoolID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_teacher_schoolID",
                table: "tb_teacher",
                column: "schoolID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_student");

            migrationBuilder.DropTable(
                name: "tb_teacher");

            migrationBuilder.DropTable(
                name: "tb_school");
        }
    }
}
