namespace Joao.Ana.Modas.App.Models.TipoPagamento
{
    public class IndexViewModel 
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Formas de Pagamentos";
        public IEnumerable<TipoPagamentoViewModel> TipoPagamentos { get; set; }

        public string Filtro { get; set; } = string.Empty;
        public int? PaginaAtual { get; set; }

        public IndexViewModel()
        {
            TipoPagamentos = new List<TipoPagamentoViewModel>();
        }
    }
}
