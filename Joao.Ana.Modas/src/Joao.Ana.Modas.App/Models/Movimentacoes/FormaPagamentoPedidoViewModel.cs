using Joao.Ana.Modas.App.Models.Pedidos;

namespace Joao.Ana.Modas.App.Models.Movimentacoes
{
    public class FormaPagamentoPedidoViewModel
    {
        public PedidoViewModel? Pedido { get; set; }

        public IEnumerable<ProdutoPedidoViewModel> Produtos { get; set; }

        public FormaPagamentoPedidoViewModel(PedidoViewModel? pedido, IEnumerable<ProdutoPedidoViewModel> produtos)
        {
            Pedido = pedido;
            Produtos = produtos;
        }

        public FormaPagamentoPedidoViewModel() 
        { 
            Produtos = Enumerable.Empty<ProdutoPedidoViewModel>();
        }  
    }
}
