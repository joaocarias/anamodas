namespace Joao.Ana.Modas.App.Models.LogistaAssociado
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Logistas";
        public LogistaAssociadoViewModel LogistaAssociado { get; set; }

        public DetalharViewModel()
        {
            LogistaAssociado = new LogistaAssociadoViewModel();
        }
    }
}
