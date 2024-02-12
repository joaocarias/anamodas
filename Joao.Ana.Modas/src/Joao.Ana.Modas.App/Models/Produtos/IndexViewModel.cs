namespace Joao.Ana.Modas.App.Models.Produtos
{
    public class IndexViewModel
    {
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }

        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Produtos";
        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            Produtos = new List<ProdutoViewModel>();
        }
    }
}
