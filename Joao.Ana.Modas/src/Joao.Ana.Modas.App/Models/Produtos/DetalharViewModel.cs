namespace Joao.Ana.Modas.App.Models.Produtos
{
    public class DetalharViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Produtos";
        public ProdutoViewModel Produto { get; set; }

        public bool? PermitirExcluir { get; set; }  

        public DetalharViewModel()
        {
            Produto = new ProdutoViewModel();
        }
    }
}
