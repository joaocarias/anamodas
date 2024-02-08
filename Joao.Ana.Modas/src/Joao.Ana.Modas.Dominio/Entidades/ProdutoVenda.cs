using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class ProdutoVenda : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; private set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal? PrecoVenda { get; private set; }

        [Required]
        public int Quantidade { get; private set; }

        [ForeignKey(nameof(CorId))]
        public Cor? Cor { get; private set; }

        public Guid? CorId { get; private set; }

        [ForeignKey(nameof(TamanhoId))]
        public Tamanho? Tamanho { get; private set; }
        
        public Guid? TamanhoId { get; private set; }

        public ProdutoVenda(string nome, decimal? precoVenda, int quantidade, Guid? corId, Guid? tamanhoId, Guid? usuarioCadastro = null) : base(usuarioCadastro)
        {
            Nome = nome;
            PrecoVenda = precoVenda;
            Quantidade = quantidade;
            CorId = corId;
            TamanhoId = tamanhoId;
        }
    }
}
