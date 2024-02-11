using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface ITipoPagamentoRepositorio : IRepositorioBase<TipoPagamento>
    {
        Task<IEnumerable<TipoPagamento>> ObterTodosPorOrdemAsync();
        Task<IEnumerable<TipoPagamento>> ObterPorNomeAsync(string filtro);
    }
}
