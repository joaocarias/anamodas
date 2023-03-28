using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.TipoPagamento
{
    public class TipoPagamentoViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        public int? Ordem { get; set; }


        public TipoPagamentoViewModel(string nome)
        {
            Nome = nome;
        }

        public TipoPagamentoViewModel() { }
    }
}
