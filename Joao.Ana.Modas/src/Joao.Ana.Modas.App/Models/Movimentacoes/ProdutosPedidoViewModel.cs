using Joao.Ana.Modas.App.Models.Pedidos;
using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.App.Models.Movimentacoes
{
    public class ProdutosPedidoViewModel
    {
        public PedidoViewModel? Pedido { get; set; }
        public Guid? PedidoId { get; set; } 

        public ProdutoPedidoViewModel Produto {  get; set; }    

        public IEnumerable<ProdutoPedidoViewModel> Produtos { get; set; } 

        public ProdutosPedidoViewModel()
        {
            Produto = new ProdutoPedidoViewModel();
            Produtos = Enumerable.Empty<ProdutoPedidoViewModel>();
        }

        public int NumeroItens
        {
            get
            {
                if (Produtos is null || !Produtos.Any()) { return 0; }
                return Produtos.Count();
            }
        }

        public int QuantiadeItens
        {
            get
            {
                if (Produtos is null || !Produtos.Any()) { return 0; }
                return Produtos.Sum(x => x.Quantidade);
            }
        }

        public decimal TotalValor
        {
            get
            {
                if (Produtos is null || !Produtos.Any()) { return 0; }
                return Produtos.Sum(x => x.PrecoVenda.GetValueOrDefault() * x.Quantidade);
            }
        }
    }
}
