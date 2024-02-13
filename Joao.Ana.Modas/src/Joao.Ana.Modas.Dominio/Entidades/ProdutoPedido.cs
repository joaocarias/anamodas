using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class ProdutoPedido : EntidadeBase
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

        [ForeignKey(nameof(ProdutoId))]
        public Produto? Produto { get; private set; }

        public Guid? ProdutoId { get; private set; }

        [ForeignKey(nameof(PedidoId))]
        public Pedido Pedido { get; private set; }

        public Guid PedidoId { get; private set; }

        public ProdutoPedido(string nome, decimal? precoVenda, int quantidade, Guid? corId, Guid? tamanhoId, Guid? produtoId, Guid pedidoId, Guid? usuarioCadastro = null) : base(usuarioCadastro)
        {
            Nome = nome;
            PrecoVenda = precoVenda;
            Quantidade = quantidade;
            CorId = corId;
            TamanhoId = tamanhoId;
            ProdutoId = produtoId;
            PedidoId = pedidoId;
        }
    }
}
