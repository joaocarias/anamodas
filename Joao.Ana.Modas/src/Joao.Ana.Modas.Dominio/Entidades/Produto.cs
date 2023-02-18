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
        
        [Required]
        [StringLength(5)]
        public string Tamanho { get; private set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Cor { get; private set; } = string.Empty;

        public Fornecedor? Fornecedor { get; private set; }
        public Guid? FornecedorId { get; private set; } = null;
                
        public Produto(string nome, double? precoUnitario, double? precoVenda, string tamanho, string cor, Guid? fornecedorId = null)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            PrecoVenda = precoVenda;

            Tamanho = tamanho;
            Cor = cor;
            FornecedorId = fornecedorId; 
        }

        private Produto() {}
    }
}
