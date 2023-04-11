using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public ProdutoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(Produto t)
        {
            try
            {
                await _appDbContext.Produtos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApagarAsync(Produto t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.Produtos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(Produto t)
        {
            try
            {
                _appDbContext.Produtos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Produto?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Produtos
                                            .Include(_ => _.Fornecedor)
                                            .Include(_ => _.LogistaAssociado)
                                            .Include(_ => _.ProdutosEstoques)
                                                .ThenInclude(_ => _.Cor)
                                            .Include(_ => _.ProdutosEstoques)
                                                .ThenInclude(_ => _.Tamanho)
                                            .Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<Produto>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.Produtos.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .Include(_ => _.Fornecedor)
                                .Include(_ => _.LogistaAssociado)
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Cor)
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Tamanho)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Produto>();
            }
        }

        public async Task<IList<Produto>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Produtos.Where(c => c.Ativo)
                                .Include(_ => _.Fornecedor) 
                                .Include(_ => _.LogistaAssociado)
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Cor)
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Tamanho)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Produto>();
            }
        }

        public async Task<IList<Produto>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Produtos.Where(c => c.Ativo)
                                .Include(_ => _.Fornecedor)
                                .Include(_ => _.LogistaAssociado)
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Cor)
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Tamanho)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Produto>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<Produto>();
            }
        }
    }
}
