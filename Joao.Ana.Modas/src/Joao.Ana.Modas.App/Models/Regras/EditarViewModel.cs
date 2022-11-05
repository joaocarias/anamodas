using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Regras
{
    public class EditarViewModel
    {
        public EditarViewModel() => Users = new List<string>();

        public string Id { get; set; }

        [Required(ErrorMessage = "O nome da Regra é obrigatório")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
