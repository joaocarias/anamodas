using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Regras
{
    public class CriarViewModel
    {
        [Required]
        [Display(Name = "Regra")]
        public string RoleName { get; set; }
    }
}
