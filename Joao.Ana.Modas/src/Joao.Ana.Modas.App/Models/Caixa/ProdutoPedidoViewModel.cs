using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.App.Models.Tamanhos;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Caixa
{
    public class ProdutoPedidoViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Preço Unitário R$")]
        [Range(0.01, double.MaxValue)]
        public decimal? PrecoVenda { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Quantidade { get; set; }

        public CorViewModel? Cor { get; set; }

        [Display(Name = "Cor")]
        public Guid CorId { get; set; }

        public TamanhoViewModel? Tamanho { get; set; }

        [Display(Name = "Tamanho")]
        public Guid TamanhoId { get; set; }

        public Guid PedidoId { get; set; }

        public PedidoViewModel? Pedido { get; set; }

        public decimal? ValorPedido
        {
            get
            {
                return Quantidade * PrecoVenda.GetValueOrDefault();
            }
        }
    }
}
