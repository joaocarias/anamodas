using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IFornecedorRepositorio : IRepositorioBase<Fornecedor>
    {
        Task<IList<Fornecedor>> ObterPorNomeAsync(string filtro);
    }
}
