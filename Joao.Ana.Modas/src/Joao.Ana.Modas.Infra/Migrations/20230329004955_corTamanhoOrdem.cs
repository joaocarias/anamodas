using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class corTamanhoOrdem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "Tamanhos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ordem",
                table: "Cores",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "Tamanhos");

            migrationBuilder.DropColumn(
                name: "Ordem",
                table: "Cores");
        }
    }
}
