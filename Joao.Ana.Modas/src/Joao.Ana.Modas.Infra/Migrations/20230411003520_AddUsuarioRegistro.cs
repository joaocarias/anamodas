using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Joao.Ana.Modas.Infra.Migrations
{
    public partial class AddUsuarioRegistro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "TipoPagamentos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "TipoPagamentos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "Tamanhos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "Tamanhos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "Produtos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "Produtos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "ProdutoEstoque",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "ProdutoEstoque",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "LogistasAssociados",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "LogistasAssociados",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "Fornecedores",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "Fornecedores",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "Enderecos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "Enderecos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "Cores",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "Cores",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioAlteracao",
                table: "Clientes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCadastro",
                table: "Clientes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "TipoPagamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "TipoPagamentos");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "Tamanhos");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "Tamanhos");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "ProdutoEstoque");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "ProdutoEstoque");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "LogistasAssociados");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "LogistasAssociados");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "Cores");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "Cores");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioCadastro",
                table: "Clientes");
        }
    }
}
