namespace Joao.Ana.Modas.App.Models.Caixa
{
    public class PedidoViewModel
    {
        public IEnumerable<ProdutoPedidoViewModel> Produtos { get; set; }

        public PedidoViewModel(IEnumerable<ProdutoPedidoViewModel> produtos)
        {
            Produtos = produtos;
        }

        public PedidoViewModel()
        {
            Produtos = new List<ProdutoPedidoViewModel>();
        }
    }
}
