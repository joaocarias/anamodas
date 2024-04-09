using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IProdutoPedidoRepositorio : IRepositorioBase<ProdutoPedido>
    {
        Task<IEnumerable<ProdutoPedido>> ProdutosPedidoAsync(Guid pedidoId);
        Task<IEnumerable<ProdutoPedido>> ProdutosPedidoByClienteIdAsync(Guid clienteId);

        Task<decimal> ValorTotalPedido(Guid pedidoId);
    }
}
