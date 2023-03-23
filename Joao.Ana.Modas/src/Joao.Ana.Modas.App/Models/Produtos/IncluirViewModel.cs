using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.App.Models.Tamanhos;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Produtos
{
    public class IncluirViewModel
    {
        public string Informacao { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public string Titulo { get; set; } = "Produtos";
        public ProdutoViewModel Produto { get; set; }
        
        [Required]
        public int Quantidade { get; set; }
        public CorViewModel Cor { get; set; }
        public Guid? CorId { get; set; }    
        public TamanhoViewModel Tamanho { get; set; }
        public Guid? TamanhoId { get; set; }

        public IncluirViewModel()
        {
            Produto = new ProdutoViewModel();
        }
    }
}
