using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class ProdutoEstoqueNuloCorETamanho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoEstoque_Cores_CorId",
                table: "ProdutoEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoEstoque_Tamanhos_TamanhoId",
                table: "ProdutoEstoque");

            migrationBuilder.AlterColumn<Guid>(
                name: "TamanhoId",
                table: "ProdutoEstoque",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "CorId",
                table: "ProdutoEstoque",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoEstoque_Cores_CorId",
                table: "ProdutoEstoque",
                column: "CorId",
                principalTable: "Cores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoEstoque_Tamanhos_TamanhoId",
                table: "ProdutoEstoque",
                column: "TamanhoId",
                principalTable: "Tamanhos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoEstoque_Cores_CorId",
                table: "ProdutoEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoEstoque_Tamanhos_TamanhoId",
                table: "ProdutoEstoque");

            migrationBuilder.AlterColumn<Guid>(
                name: "TamanhoId",
                table: "ProdutoEstoque",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "CorId",
                table: "ProdutoEstoque",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoEstoque_Cores_CorId",
                table: "ProdutoEstoque",
                column: "CorId",
                principalTable: "Cores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoEstoque_Tamanhos_TamanhoId",
                table: "ProdutoEstoque",
                column: "TamanhoId",
                principalTable: "Tamanhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
