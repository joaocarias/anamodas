using Joao.Ana.Modas.Dominio.Enums;

namespace Joao.Ana.Modas.App.Models.Caixa
{
    public class PedidoViewModel : EntidadeBaseViewModel
    {
        public ProdutoPedidoViewModel Produto { get; set; }

        public IEnumerable<ProdutoPedidoViewModel>? ProdutosPedido { get; set; }
        
        public EPeditoStatus Status { get; set; }

        public int QuantiadeItens
        {
            get
            {
                if (ProdutosPedido is null || !ProdutosPedido.Any()) { return 0; }
                return ProdutosPedido.Count();
            }
        }

        public decimal TotalValor
        {
            get
            {
                if (ProdutosPedido is null || !ProdutosPedido.Any()) { return 0; }
                return ProdutosPedido.Sum(x => x.PrecoVenda.GetValueOrDefault() * x.Quantidade);
            }
        }
    }
}
