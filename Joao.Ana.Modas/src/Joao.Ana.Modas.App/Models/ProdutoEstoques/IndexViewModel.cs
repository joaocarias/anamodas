namespace Joao.Ana.Modas.App.Models.ProdutoEstoques
{
    public class IndexViewModel
    {
        public IList<ProdutoEstoqueViewModel> ProdutoEstoques { get; set; }

        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Produtos em Estoque";
        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            ProdutoEstoques = new List<ProdutoEstoqueViewModel>();
        }
    }
}
