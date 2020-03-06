using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoShow.Migrations
{
    public partial class NovoEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Valor",
                table: "Eventos",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Eventos",
                type: "double",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
