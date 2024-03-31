namespace Joao.Ana.Modas.App.Models.Vendedores
{
    public class IndexViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Vendedores";
        public IEnumerable<VendedorViewModel> Vendedores { get; set; }

        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            Vendedores = Enumerable.Empty<VendedorViewModel>();
        }
    }
}
