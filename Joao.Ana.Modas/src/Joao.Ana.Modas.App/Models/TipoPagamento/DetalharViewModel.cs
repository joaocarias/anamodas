namespace Joao.Ana.Modas.App.Models.TipoPagamento
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Formas de Pagamentos";
        public TipoPagamentoViewModel TipoPagamento { get; set; }

        public DetalharViewModel()
        {
            TipoPagamento = new TipoPagamentoViewModel();
        }
    }
}
