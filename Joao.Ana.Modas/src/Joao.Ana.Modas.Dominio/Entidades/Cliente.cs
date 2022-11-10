using System.ComponentModel.DataAnnotations;

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

        public Endereco? Endereco { get; private set; }

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
