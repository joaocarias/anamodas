using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class EntidadeBase
    {
        [Required]
        [Key]
        public Guid Id { get; private set; }

        [Required]
        public DateTime DataCadastro { get; private set; }

        public DateTime? DataAlteracao { get; private set; } = null;

        [Required]
        public bool Ativo { get; private set; } = true;

        public EntidadeBase()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
            Ativo = true;
        }

        public void ApagarRegistro()
        {
            DataAlteracao = DateTime.Now;
            Ativo = false;
        }
    }
}
