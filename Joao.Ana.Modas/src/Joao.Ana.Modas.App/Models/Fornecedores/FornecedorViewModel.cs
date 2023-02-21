using Joao.Ana.Modas.App.Models.Enderecos;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Fornecedores
{
    public class FornecedorViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        public EnderecoViewModel? Endereco { get; set; }

        public FornecedorViewModel()
        {
            Endereco = new EnderecoViewModel();

        }       
    }
}
