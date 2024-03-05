using Joao.Ana.Modas.App.Models.Caixa;

namespace Joao.Ana.Modas.App.Models.Movimentacoes
{
    public class NovoPedidoViewModel
    {
        public PedidoViewModel Pedido { get; set; }

        public NovoPedidoViewModel() 
        {
            Pedido = new PedidoViewModel(); 
        }

        public NovoPedidoViewModel(PedidoViewModel pedido)
        {
            Pedido = pedido;
        }
    }
}
