namespace Joao.Ana.Modas.App.Models.Fornecedores
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Fornecedores";
        public FornecedorViewModel Fornecedor { get; set; }

        public DetalharViewModel()
        {
            Fornecedor = new FornecedorViewModel();
        }
    }
}
