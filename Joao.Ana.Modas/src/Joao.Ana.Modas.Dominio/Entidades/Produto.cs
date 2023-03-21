using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Produto : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; private set; } = string.Empty;

        public double? PrecoUnitario { get; private set; } = 0;

        public double? PrecoVenda { get; private set; } = 0;
        
        public IEnumerable<Tamanho> Tamanhos { get; private set; }

        public IEnumerable<Cor> Cores { get; private set; }

        public Fornecedor? Fornecedor { get; private set; }
        public Guid? FornecedorId { get; private set; } = null;
                
        public Produto(string nome, double? precoUnitario, double? precoVenda, IEnumerable<Tamanho> tamanhos, IEnumerable<Cor> cores, Guid? fornecedorId = null)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            PrecoVenda = precoVenda;

            Tamanhos = tamanhos;
            Cores = cores;
            FornecedorId = fornecedorId;
        }

        private Produto() {
            Tamanhos = new List<Tamanho>();
            Cores = new List<Cor>();
        }
    }
}
