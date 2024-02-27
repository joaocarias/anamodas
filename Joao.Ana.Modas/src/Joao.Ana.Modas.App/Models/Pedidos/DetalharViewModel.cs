using Joao.Ana.Modas.App.Models.Caixa;

namespace Joao.Ana.Modas.App.Models.Pedidos
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Pedido";
        public PedidoViewModel Pedido { get; set; }

        public DetalharViewModel()
        {
            Pedido = new PedidoViewModel();
        }
    }
}
