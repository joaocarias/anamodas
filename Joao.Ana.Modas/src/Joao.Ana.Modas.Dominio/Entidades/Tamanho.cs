using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Tamanho : EntidadeBase
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; private set; } = string.Empty;

        public int? Ordem { get; private set; }

        public Tamanho(string nome)
        {
            Nome = nome; 
        }

        private Tamanho() {
        
        }

        public void Atualizar(string nome, Guid? usuarioAlteracao)
        {
            Nome = nome;

            Atualizar(usuarioAlteracao);
        }
    }
}
