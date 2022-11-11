using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Cliente : EntidadeBase
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

        public Cliente(string nome, string? email, string? telefone, Endereco? endereco)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }

        private Cliente() { Nome = string.Empty; }
    }
}
