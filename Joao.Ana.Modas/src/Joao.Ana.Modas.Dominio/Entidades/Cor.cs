using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Cor : EntidadeBase
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; private set; } = string.Empty;
        
        public Cor(string nome)
        {
            Nome = nome;
        }

        private Cor() { 

        }
    }
}
