using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class _117 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "stateID",
                table: "tb_class",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tb_state_class",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_state_class", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_class_stateID",
                table: "tb_class",
                column: "stateID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_class_tb_state_class_stateID",
                table: "tb_class",
                column: "stateID",
                principalTable: "tb_state_class",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_class_tb_state_class_stateID",
                table: "tb_class");

            migrationBuilder.DropTable(
                name: "tb_state_class");

            migrationBuilder.DropIndex(
                name: "IX_tb_class_stateID",
                table: "tb_class");

            migrationBuilder.DropColumn(
                name: "stateID",
                table: "tb_class");
        }
    }
}
