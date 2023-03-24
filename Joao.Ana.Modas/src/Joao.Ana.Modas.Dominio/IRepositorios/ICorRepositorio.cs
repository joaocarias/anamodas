using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface ICorRepositorio : IRepositorioBase<Cor>
    {
        Task<IList<Cor>> ObterPorNomeAsync(string filtro);
    }
}
