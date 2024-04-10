namespace Joao.Ana.Modas.App.Models.Pedidos
{
    public class PedidoAbertosViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Pedidos em Abertos";
        public IEnumerable<PedidoViewModel> Pedidos { get; set; }

        public IEnumerable<ProdutoPedidoViewModel> Produtos { get; set; }   

        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public PedidoAbertosViewModel()
        {
            Pedidos = new List<PedidoViewModel>();
        }
    }
}
