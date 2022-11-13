using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IClienteRepositorio : IRepositorioBase<Cliente>
    {

        Task<IList<Cliente>> ObterPorNomeAsync(string filtro);

        Task<IList<Cliente>> ObterPorNomePaginadoAsync(string filtro, int? paginaAtual);
    }
}
