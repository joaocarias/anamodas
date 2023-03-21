using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Tamanho : EntidadeBase
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; private set; } = string.Empty;

        public Tamanho(string nome)
        {
            Nome = nome; 
        }

        private Tamanho() {
        
        }
    }
}
