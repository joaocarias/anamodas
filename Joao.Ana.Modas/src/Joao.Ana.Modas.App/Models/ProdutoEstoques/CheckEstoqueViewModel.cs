namespace Joao.Ana.Modas.App.Models.ProdutoEstoques
{
    public class CheckEstoqueViewModel
    {
        public IList<ProdutoEstoqueViewModel> ProdutoEstoques { get; set; }

        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Produtos";
        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public CheckEstoqueViewModel()
        {
            ProdutoEstoques = new List<ProdutoEstoqueViewModel>();
        }
    }
}
