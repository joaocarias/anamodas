using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models
{
    public class EntidadeBaseViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? DataAlteracao { get; set; } = null;

        [Required]
        public bool Ativo { get; set; } = true;
    }
}
