using Joao.Ana.Modas.App.Models.Fornecedores;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Produtos
{
    public class ProdutoViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        public double? PrecoUnitario { get; set; } = 0.00;

        public double? PrecoVenda { get; set; } = 0.00;

        [Required]
        [StringLength(5)]
        public string Tamanho { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Cor { get; set; } = string.Empty;

        public FornecedorViewModel? Fornecedor { get; set; }
        public Guid? FornecedorId { get; set; } = null;

       public ProdutoViewModel() { }
    }
}
