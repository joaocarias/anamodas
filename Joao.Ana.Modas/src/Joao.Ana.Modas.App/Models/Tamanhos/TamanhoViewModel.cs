using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Tamanhos
{
    public class TamanhoViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        public int? Ordem { get; set; }

        public TamanhoViewModel(string nome)
        {
            Nome = nome;
        }

        public TamanhoViewModel() { }
    }
}
