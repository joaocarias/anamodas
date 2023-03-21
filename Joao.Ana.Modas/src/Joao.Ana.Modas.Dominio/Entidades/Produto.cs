using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Produto : EntidadeBase
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; private set; } = string.Empty;

        public double? PrecoUnitario { get; private set; } = 0;

        public double? PrecoVenda { get; private set; } = 0;
                
        public Fornecedor? Fornecedor { get; private set; }
        public Guid? FornecedorId { get; private set; } = null;

        public IEnumerable<ProdutoEstoque> ProdutosEstoques { get; private set; }
                
        public Produto(string nome, double? precoUnitario, double? precoVenda, IEnumerable<ProdutoEstoque> produtosEstoques, Guid? fornecedorId = null)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            PrecoVenda = precoVenda;

            ProdutosEstoques = produtosEstoques; 

            FornecedorId = fornecedorId;
        }

        private Produto() {
            ProdutosEstoques = new List<ProdutoEstoque>();  
        }
    }
}
