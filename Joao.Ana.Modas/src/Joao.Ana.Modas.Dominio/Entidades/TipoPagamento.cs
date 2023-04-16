using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class TipoPagamento : EntidadeBase
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; private set; } = string.Empty;

        public int? Ordem { get; private set; }

        public TipoPagamento(string nome)
        {
            Nome = nome;
        }

        private TipoPagamento()
        {

        }

        public void Atualizar(string nome, int? ordem, Guid? usuarioAlteracao)
        {
            Nome = nome;
            Ordem = ordem;

            Atualizar(usuarioAlteracao);
        }
    }
}
