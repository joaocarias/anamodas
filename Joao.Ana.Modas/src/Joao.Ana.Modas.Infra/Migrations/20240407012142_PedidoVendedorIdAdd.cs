using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class PedidoVendedorIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VendedorId",
                table: "Pedidos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_VendedorId",
                table: "Pedidos",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vendedores_VendedorId",
                table: "Pedidos",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vendedores_VendedorId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_VendedorId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Pedidos");
        }
    }
}
