using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.App.Models.Tamanhos;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Caixa
{
    public class ProdutoPedidoViewModel
    {
        [Required]
        public Guid ProdutoId { get; set; }
        public ProdutoViewModel? Produto { get; set; }

        public Guid? ClienteId { get; set; }
        public ClienteViewModel? Cliente { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Quantidade { get; set; }

        public CorViewModel? Cor { get; set; }

        [Display(Name = "Cor")]
        public Guid CorId { get; set; }

        public TamanhoViewModel? Tamanho { get; set; }

        [Display(Name = "Tamanho")]
        public Guid TamanhoId { get; set; }
    }
}
