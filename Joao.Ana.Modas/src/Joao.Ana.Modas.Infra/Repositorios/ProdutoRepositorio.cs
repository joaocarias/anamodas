using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CorRepositorio> _logger;

        public ProdutoRepositorio(AppDbContext appDbContext, ILogger<CorRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Produto?> AdicionarAsync(Produto t)
        {
            try
            {
                await _appDbContext.Produtos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<Produto?> AtualizarAsync(Produto t)
        {
            try
            {
                _appDbContext.Produtos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<Produto?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Produtos
                                            .Include(_ => _.Fornecedor).AsNoTracking()
                                            .Include(_ => _.LogistaAssociado).AsNoTracking()
                                            .Include(_ => _.ProdutosEstoques)
                                                .ThenInclude(_ => _.Cor).AsNoTracking()
                                            .Include(_ => _.ProdutosEstoques)
                                                .ThenInclude(_ => _.Tamanho).AsNoTracking()
                                            .Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<Produto?> ObterSemInclusaoAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Produtos
                                            .Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<Produto>> ObterPorNomeAsync(string? filtro = null, int? limite = null)
        {
            try
            {
                var query = _appDbContext.Produtos
                                .Include(_ => _.Fornecedor).AsNoTracking()
                                .Include(_ => _.LogistaAssociado).AsNoTracking()
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Cor).AsNoTracking()
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Tamanho).AsNoTracking()
                                .AsNoTracking();
                                                

                if (!string.IsNullOrEmpty(filtro))
                {
                    query = query.Where(c => c.Nome.Contains(filtro));
                }

                query = query.Where(c => c.Ativo);

                if (limite is not null && limite > 0)
                    query = query.Take(limite.Value);
                              
                return await query.OrderBy(c => c.Nome).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Produto>();
            }
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Produtos.Where(c => c.Ativo)
                                .Include(_ => _.Fornecedor).AsNoTracking()
                                .Include(_ => _.LogistaAssociado).AsNoTracking()
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Cor).AsNoTracking()
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Tamanho).AsNoTracking()
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Produto>();
            }
        }

        public async Task<IEnumerable<Produto>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Produtos
                                .Include(_ => _.Fornecedor).AsNoTracking()
                                .Include(_ => _.LogistaAssociado).AsNoTracking()
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Cor).AsNoTracking()
                                .Include(_ => _.ProdutosEstoques)
                                    .ThenInclude(_ => _.Tamanho).AsNoTracking()
                                .Where(c => c.Ativo)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Produto>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Produto>();
            }
        }
    }
}
