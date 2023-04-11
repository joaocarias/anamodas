using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface ILogistaAssociadoRepositorio : IRepositorioBase<LogistaAssociado>
    {
        Task<IList<LogistaAssociado>> ObterPorNomeAsync(string filtro);
    }
}
