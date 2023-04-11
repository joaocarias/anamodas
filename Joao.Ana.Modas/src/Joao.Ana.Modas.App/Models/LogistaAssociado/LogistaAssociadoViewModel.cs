using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.LogistaAssociado
{
    public class LogistaAssociadoViewModel : EntidadeBaseViewModel
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [StringLength(200)]
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        public LogistaAssociadoViewModel(string nome, string nomeFantasia, string? email, string? telefone)
        {
            Nome = nome;
            NomeFantasia = nomeFantasia;
            Email = email;
            Telefone = telefone;
        }

        public LogistaAssociadoViewModel() { }
    }
}
