namespace Joao.Ana.Modas.App.Models.Clientes
{
    public class IndexViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty ;
        public string Titulo { get; set; } = "Clientes";
        public IEnumerable<ClienteViewModel> Clientes { get; set; }

        public string Filtro { get; set; } = string.Empty ;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            Clientes = new List<ClienteViewModel>();
        } 
    }
}
