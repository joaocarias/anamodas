using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Usuarios
{
    public class EditarViewModel
    {
        public EditarViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }


        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O nome email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public string Nome { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}