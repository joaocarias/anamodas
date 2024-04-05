using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IVendedorRepositorio : IRepositorioBase<Vendedor>
    {
        Task<IEnumerable<Vendedor>> ObterPorNomeAsync(string filtro);
    }
}
