using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class ProdutoEstoqueRepositorio : IProdutoEstoqueRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CorRepositorio> _logger;

        public ProdutoEstoqueRepositorio(AppDbContext appDbContext, ILogger<CorRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<ProdutoEstoque?> AdicionarAsync(ProdutoEstoque t)
        {
            try
            {
                await _appDbContext.ProdutoEstoque.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(ProdutoEstoque t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.ProdutoEstoque.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<ProdutoEstoque?> AtualizarAsync(ProdutoEstoque t)
        {
            try
            {
                _appDbContext.ProdutoEstoque.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<ProdutoEstoque?> GetInclusoAsync(Guid ProdutoId, Guid CorId, Guid TamanhoId)
        {
            try
            {
                var c = await _appDbContext.ProdutoEstoque
                                                .Where(_ => _.Ativo 
                                                        && _.ProdutoId.Equals(ProdutoId)
                                                        && _.CorId.Equals(CorId)
                                                        && _.TamanhoId.Equals(TamanhoId)
                                                )                                                
                                                .AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<ProdutoEstoque?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.ProdutoEstoque
                        .Include(c => c.Produto.Fornecedor)
                                .Include(c => c.Produto.LogistaAssociado).AsNoTracking()
                                .Include(c => c.Cor).AsNoTracking()
                                .Include(c => c.Tamanho).AsNoTracking()
                    .Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<ProdutoEstoque>> ObterPorFiltro(string filtro)
        {
            var l = _appDbContext.ProdutoEstoque
                                .Include(c => c.Produto.Fornecedor)
                                .Include(c => c.Produto.LogistaAssociado)
                                .Include(c => c.Cor)
                                .Include(c => c.Tamanho)
                               .Where(c => c.Ativo)
                               .AsNoTracking();                               

            if (!string.IsNullOrEmpty(filtro))
            {
                l = l.Where(c =>
                            (c.Produto != null && c.Produto.Nome.Contains(filtro))
                            || (c.Cor != null && c.Cor.Nome.Contains(filtro))
                            || (c.Tamanho != null && c.Tamanho.Nome.Contains(filtro))
                    );
            }

            return await l.OrderBy(c => c.Produto.Nome).ThenBy(c => c.Cor.Nome).ToListAsync();
        }

        public async Task<IEnumerable<ProdutoEstoque>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.ProdutoEstoque
                                .Include(c => c.Produto.Fornecedor)
                                .Include(c => c.Produto.LogistaAssociado)
                                .Include(c => c.Cor)
                                .Include(c => c.Tamanho)
                                .Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Produto.Nome)
                                .ThenBy(c => c.Cor.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ProdutoEstoque>();
            }
        }

        public async Task<IEnumerable<ProdutoEstoque>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.ProdutoEstoque.Where(c => c.Ativo).OrderBy(x => x.ProdutoId);
                return await Paginacao<ProdutoEstoque>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ProdutoEstoque>();
            }
        }
    }
}
