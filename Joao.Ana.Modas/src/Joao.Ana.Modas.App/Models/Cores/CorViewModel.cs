using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Cores
{
    public class CorViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        public CorViewModel(string nome)
        {
            Nome = nome;
        }

        public CorViewModel() { }
    }
}
