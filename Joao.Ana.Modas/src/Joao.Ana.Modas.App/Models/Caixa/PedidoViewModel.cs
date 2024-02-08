namespace Joao.Ana.Modas.App.Models.Caixa
{
    public class PedidoViewModel
    {
        public IEnumerable<ProdutoPedidoViewModel> Produtos { get; set; }
        public ProdutoPedidoViewModel ProdutoPedido { get; set; }

        public PedidoViewModel(IEnumerable<ProdutoPedidoViewModel> produtos, ProdutoPedidoViewModel produtoPedido)
        {
            Produtos = produtos;
            ProdutoPedido = produtoPedido;
        }

        public PedidoViewModel()
        {
            Produtos = new List<ProdutoPedidoViewModel>();
            
        }
    }
}
