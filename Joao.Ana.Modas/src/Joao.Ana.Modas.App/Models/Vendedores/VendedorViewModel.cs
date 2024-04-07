using Joao.Ana.Modas.Dominio.Enums;
using System.ComponentModel.DataAnnotations;
using Joao.Ana.Modas.Dominio.Extensoes;

namespace Joao.Ana.Modas.App.Models.Vendedores
{
    public class VendedorViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        [Display(Name = "Tipo de Comissão")]
        public ETipoComissao TipoComissao { get; set; }

        [Display(Name = "Comissão")]
        public decimal? Comissao { get; set; }
                
        public VendedorViewModel(string nome, string? email = null, string? telefone = null, ETipoComissao tipoComissao = ETipoComissao.Desconhecido, decimal? comissao = null)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            TipoComissao = tipoComissao;
            Comissao = comissao;
        }

        public VendedorViewModel()
        {
        }

        public string TipoComissaoDescricao => TipoComissao.ToStringParse();

        public string? ComissaoFormatodo => Comissao.DecimalToString();
    }
}
