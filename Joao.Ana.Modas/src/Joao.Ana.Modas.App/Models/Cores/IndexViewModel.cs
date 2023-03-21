namespace Joao.Ana.Modas.App.Models.Cores
{
    public class IndexViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Cores";
        public IList<CorViewModel> Tamanhos { get; set; }

        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            Tamanhos = new List<CorViewModel>();
        }
    }
}
