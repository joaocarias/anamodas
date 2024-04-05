using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class VendedorRepositorio : IVendedorRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<VendedorRepositorio> _logger;

        public VendedorRepositorio(AppDbContext appDbContext, ILogger<VendedorRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Vendedor?> AdicionarAsync(Vendedor t)
        {
            try
            {
                await _appDbContext.Vendedores.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(Vendedor t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.Vendedores.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<Vendedor?> AtualizarAsync(Vendedor t)
        {
            try
            {
                _appDbContext.Vendedores.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<Vendedor?> ObterAsync(Guid id)
        {
            try
            {
                return await _appDbContext.Vendedores.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<Vendedor>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                return await _appDbContext.Vendedores.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Vendedor>();
            }
        }

        public async Task<IEnumerable<Vendedor>> ObterTodosAsync()
        {
            try
            {
                return await _appDbContext.Vendedores.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Vendedor>();
            }
        }

        public async Task<IEnumerable<Vendedor>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Vendedores.Where(c => c.Ativo)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Vendedor>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Vendedor>();
            }
        }
    }
}
