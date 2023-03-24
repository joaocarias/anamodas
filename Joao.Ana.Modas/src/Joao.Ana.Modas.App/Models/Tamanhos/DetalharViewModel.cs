namespace Joao.Ana.Modas.App.Models.Tamanhos
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Tamanhos";
        public TamanhoViewModel Tamanho { get; set; }

        public DetalharViewModel()
        {
            Tamanho = new TamanhoViewModel();
        }
    }
}
