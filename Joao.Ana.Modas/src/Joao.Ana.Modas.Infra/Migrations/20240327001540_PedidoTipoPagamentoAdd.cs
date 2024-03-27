using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class PedidoTipoPagamentoAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TipoPagamentoId",
                table: "Pedidos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_TipoPagamentoId",
                table: "Pedidos",
                column: "TipoPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_TipoPagamentos_TipoPagamentoId",
                table: "Pedidos",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamentos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_TipoPagamentos_TipoPagamentoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_TipoPagamentoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "TipoPagamentoId",
                table: "Pedidos");
        }
    }
}
