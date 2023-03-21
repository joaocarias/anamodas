using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.App.Models.Tamanhos;

namespace Joao.Ana.Modas.App.Models.ProdutoEstoques
{
    public class ProdutoEstoqueViewModel : EntidadeBaseViewModel
    {
        public ProdutoViewModel Produto { get; set; }
        public Guid ProdutoId { get; set; }

        public CorViewModel Cor { get; set; }
        public Guid CorId { get; set; }

        public TamanhoViewModel Tamanho { get; private set; }
        public Guid TamanhoId { get; private set; }

        public int Quantidade { get; private set; }

        public ProdutoEstoqueViewModel(Guid produtoId, Guid corId, Guid tamanhoId, int quantidade)
        {
            ProdutoId = produtoId;
            CorId = corId;
            TamanhoId = tamanhoId;
            Quantidade = quantidade;
        }

        private ProdutoEstoqueViewModel() { }
    }
}
