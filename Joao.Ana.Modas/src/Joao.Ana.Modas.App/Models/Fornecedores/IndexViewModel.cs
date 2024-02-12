namespace Joao.Ana.Modas.App.Models.Fornecedores
{
    public class IndexViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Fornecedores";
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }

        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            Fornecedores = new List<FornecedorViewModel>();
        }
    }
}
