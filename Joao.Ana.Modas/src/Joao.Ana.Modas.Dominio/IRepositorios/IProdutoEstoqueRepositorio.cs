using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IProdutoEstoqueRepositorio : IRepositorioBase<ProdutoEstoque>
    {
        Task<ProdutoEstoque?> GetInclusoAsync(Guid ProdutoId, Guid CorId, Guid TamanhoId);

        Task<IEnumerable<ProdutoEstoque>> ObterPorFiltro(string filtro);
    }
}
