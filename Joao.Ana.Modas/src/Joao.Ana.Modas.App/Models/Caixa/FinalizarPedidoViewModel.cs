namespace Joao.Ana.Modas.App.Models.Caixa
{
    public class FinalizarPedidoViewModel
    {
        public Guid PedidoId { get; set; }
       
        public PedidoViewModel Pedido { get; set; } = new PedidoViewModel();

        public FinalizarPedidoViewModel(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public FinalizarPedidoViewModel()
        {
        }
    }
}
