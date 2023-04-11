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

        public Guid? UsuarioCadastro { get; private set; }

        public Guid? UsuarioAlteracao { get; private set; }

        public EntidadeBase(Guid? usuarioCadastro = null)
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
            Ativo = true;
            UsuarioCadastro = usuarioCadastro;
        }

        public void ApagarRegistro(Guid? usuarioAlteracao = null)
        {
            DataAlteracao = DateTime.Now;
            Ativo = false;
            UsuarioAlteracao = usuarioAlteracao;
        }

        public void Atualizar(Guid? usuarioAlteracao = null)
        {
            DataAlteracao = DateTime.Now;
            UsuarioAlteracao = usuarioAlteracao;
        }
    }
}
