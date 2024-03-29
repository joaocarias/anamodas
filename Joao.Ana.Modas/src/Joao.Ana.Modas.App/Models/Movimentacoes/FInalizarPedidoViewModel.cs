using Joao.Ana.Modas.App.Models.Pedidos;

namespace Joao.Ana.Modas.App.Models.Movimentacoes
{
    public class FInalizarPedidoViewModel
    {
        public PedidoViewModel? Pedido { get; set; }

        public IEnumerable<ProdutoPedidoViewModel> Produtos { get; set; }
        
        public FInalizarPedidoViewModel(PedidoViewModel? pedido, IEnumerable<ProdutoPedidoViewModel> produtos)
        {
            Pedido = pedido;
            Produtos = produtos;
        }

        public FInalizarPedidoViewModel()
        {
            Produtos = Enumerable.Empty<ProdutoPedidoViewModel>();
        }
    }
}
