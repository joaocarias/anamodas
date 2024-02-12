namespace Joao.Ana.Modas.App.Models.LogistaAssociado
{
    public class IndexViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Logistas";
        public IEnumerable<LogistaAssociadoViewModel> LogistasAssociados { get; set; }

        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            LogistasAssociados = new List<LogistaAssociadoViewModel>();
        }
    }
}
