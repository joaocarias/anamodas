using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.App.Models.Fornecedores;
using Joao.Ana.Modas.App.Models.Tamanhos;
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

        public IEnumerable<TamanhoViewModel> Tamanhos { get; set; }

        public IEnumerable<CorViewModel> Cores { get; set; }

        public FornecedorViewModel? Fornecedor { get; set; }
        public Guid? FornecedorId { get; set; } = null;

       public ProdutoViewModel() {
            Tamanhos = new List<TamanhoViewModel>();
            Cores = new List<CorViewModel>();
        }
    }
}
