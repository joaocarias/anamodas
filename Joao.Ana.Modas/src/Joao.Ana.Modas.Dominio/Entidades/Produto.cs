using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Produto : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; private set; } 
        
        public double? PrecoUnitario { get; private set; }
        
        public double? PrecoVenda { get; private set; }
        
        [Required]
        [StringLength(10)]
        public string Tamanho { get; private set; }

        public Produto(string nome, double? precoUnitario, double? precoVenda, string tamanho)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            PrecoVenda = precoVenda;
            Tamanho = tamanho;
        }

        private Produto() {
            Nome = string.Empty;
            Tamanho= string.Empty;  
        }
    }
}
