namespace Joao.Ana.Modas.App.Models.Vendedores
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Vendedor";
        public VendedorViewModel Vendedor { get; set; }

        public DetalharViewModel()
        {
            Vendedor = new VendedorViewModel();
        }
    }
}
