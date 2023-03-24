namespace Joao.Ana.Modas.App.Models.Cores
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Cores";
        public CorViewModel Cor { get; set; }

        public DetalharViewModel()
        {
            Cor = new CorViewModel();
        }
    }
}
