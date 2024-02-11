using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface ITamanhoRepositorio : IRepositorioBase<Tamanho>
    {
        Task<IEnumerable<Tamanho>> ObterPorNomeAsync(string filtro);
        Task<IEnumerable<Tamanho>> ObterTodosPorOrdemAsync();
    }
}
