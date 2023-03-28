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
        public ProdutoViewModel? Produto { get; set; }
        public Guid ProdutoId { get; set; } 
        
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Quantidade { get; set; }
        
        public CorViewModel? Cor { get; set; }

        [Display(Name = "Cor")]
        public Guid CorId { get; set; }    
        
        public TamanhoViewModel? Tamanho { get; set; }

        [Display(Name = "Tamanho")]
        public Guid TamanhoId { get; set; }

        public IncluirViewModel()
        {
            
        }
    }
}
