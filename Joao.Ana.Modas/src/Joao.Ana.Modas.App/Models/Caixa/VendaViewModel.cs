using Joao.Ana.Modas.Dominio.Enums;

namespace Joao.Ana.Modas.App.Models.Caixa
{
    public class VendaViewModel : EntidadeBaseViewModel
    {
        public ProdutoVendaViewModel Produto { get; set; }

        public IEnumerable<ProdutoVendaViewModel>? ProdutosVenda { get; set; }

        public EVendaStatus Status { get; set; }

        public int QuantiadeItens
        {
            get
            {
                if (ProdutosVenda is null || !ProdutosVenda.Any()) { return 0; }
                return ProdutosVenda.Count();
            }
        }

        public decimal TotalValor
        {
            get
            {
                if (ProdutosVenda is null || !ProdutosVenda.Any()) { return 0; }
                return ProdutosVenda.Sum(x => x.PrecoVenda.GetValueOrDefault() * x.Quantidade);
            }
        }
    }
}
