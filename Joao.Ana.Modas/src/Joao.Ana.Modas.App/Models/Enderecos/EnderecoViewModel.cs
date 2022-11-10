using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Enderecos
{
    public class EnderecoViewModel
    {
        [StringLength(100)]
        public string? Logradouro { get; private set; }

        [StringLength(10)]
        public string? Numero { get; private set; }

        [StringLength(100)]
        public string? Bairro { get; private set; }

        [StringLength(100)]
        public string? Complemento { get; private set; }

        [StringLength(20)]
        public string? Cep { get; private set; }

        [StringLength(100)]
        public string? Cidade { get; private set; }

        [StringLength(2)]
        public string? Estado { get; private set; }

    }
}
