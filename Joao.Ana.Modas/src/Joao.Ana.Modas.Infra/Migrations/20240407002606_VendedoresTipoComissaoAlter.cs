using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class VendedoresTipoComissaoAlter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoComissiao",
                table: "Vendedores");

            migrationBuilder.AddColumn<int>(
                name: "TipoComissao",
                table: "Vendedores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoComissao",
                table: "Vendedores");

            migrationBuilder.AddColumn<int>(
                name: "TipoComissiao",
                table: "Vendedores",
                type: "int",
                nullable: true);
        }
    }
}
