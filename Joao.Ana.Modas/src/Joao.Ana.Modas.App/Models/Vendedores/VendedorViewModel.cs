using Joao.Ana.Modas.Dominio.Enums;
using System.ComponentModel.DataAnnotations;
using Joao.Ana.Modas.Dominio.Extensoes;

namespace Joao.Ana.Modas.App.Models.Vendedores
{
    public class VendedorViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; }
               
        [StringLength(200)]
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        [Display(Name = "Tipo de Comissão")]
        public ETipoComissiao? TipoComissiao { get; set; }

        [Display(Name = "Comissão")]
        public decimal? Comissao { get; set; }
                
        public VendedorViewModel(string nome, string? email = null, string? telefone = null, ETipoComissiao? tipoComissiao = null, decimal? comissao = null)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            TipoComissiao = tipoComissiao;
            Comissao = comissao;
        }

        public VendedorViewModel()
        {
        }

        public string TipoComissaoDescricao => TipoComissiao.ToStringParse();

        public string? ComissaoFormatodo => Comissao.DecimalToString();
    }
}
