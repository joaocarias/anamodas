using Joao.Ana.Modas.App.Models.Pedidos;

namespace Joao.Ana.Modas.App.Models.Movimentacoes
{
    public class AdicionarProdutoPedidoViewModel
    {
        public Guid? PedidoId { get; set; }
        public ProdutoPedidoViewModel Produto { get; set; }
    }
}
