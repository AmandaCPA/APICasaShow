using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoShow.Migrations
{
    public partial class Floatevento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Eventos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Valor",
                table: "Eventos",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
