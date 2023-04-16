using Joao.Ana.Modas.App.Models.Fornecedores;
using Joao.Ana.Modas.App.Models.LogistaAssociado;
using Joao.Ana.Modas.App.Models.ProdutoEstoques;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Joao.Ana.Modas.App.Models.Produtos
{
    public class ProdutoViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Preço Unitário R$")]
        [Range(0.01, double.MaxValue)]
        public decimal? PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Preço De Venda R$")]
        [Range(0.01, double.MaxValue)]
        public decimal? PrecoVenda { get; set; }

        public FornecedorViewModel? Fornecedor { get; set; }

        [Display(Name = "Fornecedor")]
        public Guid? FornecedorId { get; set; } = null;

        public LogistaAssociadoViewModel? LogistaAssociado { get; set; } = null;

        [Display(Name = "Logista")]
        public Guid? LogistaAssociadoId { get; set; } = null;

        public IEnumerable<ProdutoEstoqueViewModel> ProdutosEstoques { get; set; }

        public ProdutoViewModel() {
            ProdutosEstoques = new List<ProdutoEstoqueViewModel>(); 
        }

        public string? PrecoUnitarioFormatodo
        {
            get {
                    return PrecoUnitario is null ? "0,00" : PrecoUnitario?.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
                }
        }

        public string? PrecoVendaFormatodo
        {
            get
            {
                return PrecoVenda is null ? "0,00" : PrecoVenda?.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
            }
        }
    }
}
