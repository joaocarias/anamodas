using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface ITamanhoRepositorio : IRepositorioBase<Tamanho>
    {
        Task<IList<Tamanho>> ObterPorNomeAsync(string filtro);
        Task<IList<Tamanho>> ObterTodosPorOrdemAsync();
    }
}
