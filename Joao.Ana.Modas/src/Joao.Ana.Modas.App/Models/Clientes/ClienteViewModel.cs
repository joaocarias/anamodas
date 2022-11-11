using Joao.Ana.Modas.App.Models.Enderecos;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Clientes
{
    public class ClienteViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        public EnderecoViewModel? Endereco { get; set; }

        public ClienteViewModel()
        {
            Endereco = new EnderecoViewModel();
        }
    }
}
