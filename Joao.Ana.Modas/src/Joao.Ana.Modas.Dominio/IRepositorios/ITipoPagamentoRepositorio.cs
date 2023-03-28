using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface ITipoPagamentoRepositorio : IRepositorioBase<TipoPagamento>
    {
        Task<IList<TipoPagamento>> ObterTodosPorOrdemAsync();
        Task<IList<TipoPagamento>> ObterPorNomeAsync(string filtro);
    }
}
