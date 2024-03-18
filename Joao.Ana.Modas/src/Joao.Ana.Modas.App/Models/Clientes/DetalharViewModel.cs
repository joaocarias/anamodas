using Joao.Ana.Modas.App.Models.Pedidos;

namespace Joao.Ana.Modas.App.Models.Clientes
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Clientes";
        public ClienteViewModel Cliente { get; set; }

        public bool? PermitirExcluir { get; set; }

        public bool? PermitirVerCompras { get; set; }   

        public IEnumerable<PedidoViewModel> Pedidos { get; set; }

        public IEnumerable<ProdutoPedidoViewModel> Produtos { get; set; }   

        public DetalharViewModel()
        {
            Cliente = new ClienteViewModel();
            Pedidos = Enumerable.Empty<PedidoViewModel>();   
            Produtos = Enumerable.Empty<ProdutoPedidoViewModel>(); 
        }
    }
}
