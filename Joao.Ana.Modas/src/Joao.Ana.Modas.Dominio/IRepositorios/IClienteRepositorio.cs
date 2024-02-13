using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IClienteRepositorio : IRepositorioBase<Cliente>
    {

        Task<IEnumerable<Cliente>> ObterPorNomeAsync(string filtro, int? limite = null);

        Task<IEnumerable<Cliente>> ObterPorNomePaginadoAsync(string filtro, int? paginaAtual);
    }
}
