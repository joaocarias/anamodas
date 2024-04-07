using Joao.Ana.Modas.Dominio.Enums;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Vendedor : EntidadeBase
    {
        public string Nome { get; private set; }

        public string? Email { get; private set; }

        public string? Telefone { get; private set; }

        public ETipoComissao TipoComissao { get; private set; }

        public decimal? Comissao { get; private set; }

        public Vendedor(string nome, string? email = null, string? telefone = null, ETipoComissao tipoComissao = ETipoComissao.Desconhecido, decimal? comissao = null, Guid? usuarioCadastro = null) : base(usuarioCadastro) 
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            TipoComissao = tipoComissao;
            Comissao = comissao;
        }

        private Vendedor() { }

        public void Atualizar(string nome, string? email = null, string? telefone = null, ETipoComissao tipoComissao = ETipoComissao.Desconhecido, decimal? comissao = null, Guid? usuarioAlteracao = null)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            TipoComissao = tipoComissao;
            Comissao = comissao;

            Atualizar(usuarioAlteracao); 
        }
    }
}
