namespace Joao.Ana.Modas.App.Models.Pedidos
{
    public class AdicionarProdutoPedidoViewModel
    {
        public Guid? PedidoId { get; set; }
        public Guid? ProdutoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Guid? CorId { get; set; }
        public Guid? TamanhoId { get; set; }
        public int Quantidade {  get; set; } = 0;
        public decimal? PrecoVenda { get; set; }
    }
}
