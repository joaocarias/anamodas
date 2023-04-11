using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class LogistaAssociado : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; private set; }

        [Required]
        [StringLength(200)]
        public string NomeFantasia { get; private set; }

        [StringLength(200)]
        public string? Email { get; private set; }

        [StringLength(20)]
        public string? Telefone { get; private set; }

        public LogistaAssociado(string nome, string nomeFantasia, string? email, string? telefone)
        {
            Nome = nome;
            NomeFantasia = nomeFantasia;
            Email = email;
            Telefone = telefone;
        }

        private LogistaAssociado() { }
    }
}
