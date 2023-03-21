using Joao.Ana.Modas.App.Models.Fornecedores;
using Joao.Ana.Modas.App.Models.ProdutoEstoques;
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

        public FornecedorViewModel? Fornecedor { get; set; }
        public Guid? FornecedorId { get; set; } = null;

        public IEnumerable<ProdutoEstoqueViewModel> ProdutosEstoques { get; set; }

        public ProdutoViewModel() {
            ProdutosEstoques = new List<ProdutoEstoqueViewModel>(); 
        }
    }
}
