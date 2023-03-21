using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class CoresETamanhosProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorProduto",
                columns: table => new
                {
                    CoresId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdutosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorProduto", x => new { x.CoresId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_CorProduto_Cores_CoresId",
                        column: x => x.CoresId,
                        principalTable: "Cores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorProduto_Produtos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProdutoTamanho",
                columns: table => new
                {
                    ProdutosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TamanhosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoTamanho", x => new { x.ProdutosId, x.TamanhosId });
                    table.ForeignKey(
                        name: "FK_ProdutoTamanho_Produtos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoTamanho_Tamanhos_TamanhosId",
                        column: x => x.TamanhosId,
                        principalTable: "Tamanhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CorProduto_ProdutosId",
                table: "CorProduto",
                column: "ProdutosId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoTamanho_TamanhosId",
                table: "ProdutoTamanho",
                column: "TamanhosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorProduto");

            migrationBuilder.DropTable(
                name: "ProdutoTamanho");
        }
    }
}
