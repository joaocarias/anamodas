using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Fornecedor : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; private set; }

        [StringLength(200)]
        public string? Email { get; private set; }

        [StringLength(20)]
        public string? Telefone { get; private set; }

        [ForeignKey(nameof(EnderecoId))]
        public Endereco? Endereco { get; private set; }

        public Guid? EnderecoId { get; private set; }

        public Fornecedor(string nome, string? email, string? telefone, Endereco? endereco)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

        private Fornecedor() { Nome = string.Empty; }

        public void Atualizar(string nome, string? email, string? telefone, Endereco? endereco, Guid? usuarioAlteracao)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;

            Atualizar(usuarioAlteracao);
        }
    }
}
