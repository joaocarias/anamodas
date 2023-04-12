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

        public LogistaAssociado? LogistaAssociado { get; private set; } = null;
        public Guid? LogistaAssociadoId { get; private set; } = null;

        public IEnumerable<ProdutoEstoque> ProdutosEstoques { get; private set; }
                
        public Produto(string nome, double? precoUnitario, double? precoVenda, IEnumerable<ProdutoEstoque> produtosEstoques, Guid? fornecedorId = null, Guid? logistaAssociadoId = null)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            PrecoVenda = precoVenda;

            ProdutosEstoques = produtosEstoques; 

            FornecedorId = fornecedorId;
            LogistaAssociadoId = logistaAssociadoId;
        }

        private Produto() {
            ProdutosEstoques = new List<ProdutoEstoque>();  
        }

        public void Atualizar(string nome, double? precoUnitario, double? precoVenda, Guid? fornecedorId = null, Guid? logistaAssociadoId = null, Guid? usuarioAlteracao = null)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            PrecoVenda = precoVenda;

            FornecedorId = fornecedorId;
            LogistaAssociadoId = logistaAssociadoId;

            Atualizar(usuarioAlteracao);
        }
    }
}
