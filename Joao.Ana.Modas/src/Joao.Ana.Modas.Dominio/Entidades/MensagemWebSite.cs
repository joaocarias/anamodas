using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class MensagemWebSite : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; private set; } = string.Empty;

        [StringLength(200)]
        public string? Email { get; private set; }
                
        [StringLength(20)]
        public string? Telefone { get; private set; }
               
        [StringLength(1000)]
        public string? Mensagem { get; private set; }

        private MensagemWebSite() { }

        public MensagemWebSite(string nome, string? email, string? telefone, string? mensagem)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Mensagem = mensagem;
        }
    }
}
