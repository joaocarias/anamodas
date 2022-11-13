using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Enderecos
{
    public class EnderecoViewModel : EntidadeBaseViewModel
    {
        [StringLength(100)]
        public string? Logradouro { get; set; }

        [StringLength(10)]
        public string? Numero { get; set; }

        [StringLength(100)]
        public string? Bairro { get; set; }

        [StringLength(100)]
        public string? Complemento { get; set; }

        [StringLength(20)]
        public string? Cep { get; set; }

        [StringLength(100)]
        public string? Cidade { get; set; }

        [StringLength(2)]
        public string? Estado { get; set; }

        public EnderecoViewModel()
        {

        }

    }
}
