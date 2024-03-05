using Joao.Ana.Modas.App.Models.Caixa;
using Joao.Ana.Modas.App.Models.Clientes;

namespace Joao.Ana.Modas.App.Models.Movimentacoes
{
    public class RegistrarPedidoViewModel
    {
        public Guid? PedidoId { get; set; }

        public PedidoViewModel? Pedido { get; set; }

        public Guid? ClienteId { get; set; }

        public ClienteViewModel? Cliente { get; set; }

        public ProdutoPedidoViewModel? Produto { get; set; }

        public IEnumerable<ProdutoPedidoViewModel> ProdutosPedido { get; set; }
                       
        public FinalizarPedidoViewModel Confirmar { get; set; } = new FinalizarPedidoViewModel();
        public FinalizarPedidoViewModel Cancelar { get; set; } = new FinalizarPedidoViewModel();

        public RegistrarPedidoViewModel()
        {
            ProdutosPedido = Enumerable.Empty<ProdutoPedidoViewModel>();
        }

        public RegistrarPedidoViewModel(Guid? pedidoId, PedidoViewModel? pedido)
        {
            PedidoId = pedidoId;
            Pedido = pedido;

            ProdutosPedido = Enumerable.Empty<ProdutoPedidoViewModel>();
        }

        public RegistrarPedidoViewModel(PedidoViewModel? pedido)
        {
            PedidoId = pedido?.Id;
            Pedido = pedido;

            ProdutosPedido = Enumerable.Empty<ProdutoPedidoViewModel>();
        }

        public int NumeroItens
        {
            get
            {
                if (ProdutosPedido is null || !ProdutosPedido.Any()) { return 0; }
                return ProdutosPedido.Count();
            }
        }

        public int QuantiadeItens
        {
            get
            {
                if (ProdutosPedido is null || !ProdutosPedido.Any()) { return 0; }
                return ProdutosPedido.Sum(x => x.Quantidade);
            }
        }

        public decimal TotalValor
        {
            get
            {
                if (ProdutosPedido is null || !ProdutosPedido.Any()) { return 0; }
                return ProdutosPedido.Sum(x => x.PrecoVenda.GetValueOrDefault() * x.Quantidade);
            }
        }
    }
}
