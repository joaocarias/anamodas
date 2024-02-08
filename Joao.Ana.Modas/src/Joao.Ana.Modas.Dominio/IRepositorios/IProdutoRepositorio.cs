using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IProdutoRepositorio : IRepositorioBase<Produto>
    {
        Task<IEnumerable<Produto>> ObterPorNomeAsync(string? filtro = null, int? limite = null);
        Task<Produto?> ObterSemInclusaoAsync(Guid id);
    }
}
