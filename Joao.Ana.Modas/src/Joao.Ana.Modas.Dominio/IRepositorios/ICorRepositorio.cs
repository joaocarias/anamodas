using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface ICorRepositorio : IRepositorioBase<Cor>
    {
        Task<IEnumerable<Cor>> ObterPorNomeAsync(string filtro);
    }
}
