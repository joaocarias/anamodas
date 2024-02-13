using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class ProdutosPedidoTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoPedido_Cores_CorId",
                table: "ProdutoPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoPedido_Pedidos_PedidoId",
                table: "ProdutoPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoPedido_Produtos_ProdutoId",
                table: "ProdutoPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoPedido_Tamanhos_TamanhoId",
                table: "ProdutoPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoPedido",
                table: "ProdutoPedido");

            migrationBuilder.RenameTable(
                name: "ProdutoPedido",
                newName: "ProdutosPedido");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoPedido_TamanhoId",
                table: "ProdutosPedido",
                newName: "IX_ProdutosPedido_TamanhoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoPedido_ProdutoId",
                table: "ProdutosPedido",
                newName: "IX_ProdutosPedido_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoPedido_PedidoId",
                table: "ProdutosPedido",
                newName: "IX_ProdutosPedido_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoPedido_CorId",
                table: "ProdutosPedido",
                newName: "IX_ProdutosPedido_CorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PedidoId",
                table: "ProdutosPedido",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutosPedido",
                table: "ProdutosPedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPedido_Cores_CorId",
                table: "ProdutosPedido",
                column: "CorId",
                principalTable: "Cores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPedido_Pedidos_PedidoId",
                table: "ProdutosPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPedido_Produtos_ProdutoId",
                table: "ProdutosPedido",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPedido_Tamanhos_TamanhoId",
                table: "ProdutosPedido",
                column: "TamanhoId",
                principalTable: "Tamanhos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPedido_Cores_CorId",
                table: "ProdutosPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPedido_Pedidos_PedidoId",
                table: "ProdutosPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPedido_Produtos_ProdutoId",
                table: "ProdutosPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPedido_Tamanhos_TamanhoId",
                table: "ProdutosPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutosPedido",
                table: "ProdutosPedido");

            migrationBuilder.RenameTable(
                name: "ProdutosPedido",
                newName: "ProdutoPedido");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosPedido_TamanhoId",
                table: "ProdutoPedido",
                newName: "IX_ProdutoPedido_TamanhoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosPedido_ProdutoId",
                table: "ProdutoPedido",
                newName: "IX_ProdutoPedido_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosPedido_PedidoId",
                table: "ProdutoPedido",
                newName: "IX_ProdutoPedido_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosPedido_CorId",
                table: "ProdutoPedido",
                newName: "IX_ProdutoPedido_CorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PedidoId",
                table: "ProdutoPedido",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoPedido",
                table: "ProdutoPedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoPedido_Cores_CorId",
                table: "ProdutoPedido",
                column: "CorId",
                principalTable: "Cores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoPedido_Pedidos_PedidoId",
                table: "ProdutoPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoPedido_Produtos_ProdutoId",
                table: "ProdutoPedido",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoPedido_Tamanhos_TamanhoId",
                table: "ProdutoPedido",
                column: "TamanhoId",
                principalTable: "Tamanhos",
                principalColumn: "Id");
        }
    }
}
