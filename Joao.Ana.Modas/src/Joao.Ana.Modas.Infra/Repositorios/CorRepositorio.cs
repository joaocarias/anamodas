using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class CorRepositorio : ICorRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CorRepositorio> _logger;

        public CorRepositorio(AppDbContext appDbContext, ILogger<CorRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Cor?> AdicionarAsync(Cor t)
        {
            try
            {
                await _appDbContext.Cores.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(Cor t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.Cores.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);  
                return false;
            }
        }

        public async Task<Cor?> AtualizarAsync(Cor t)
        {
            try
            {
                _appDbContext.Cores.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message , ex);  
                return null;
            }
        }

        public async Task<Cor?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Cores.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);  
                return null;
            }
        }

        public async Task<IEnumerable<Cor>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.Cores.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Cor>();
            }
        }

        public async Task<IEnumerable<Cor>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Cores.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Cor>();
            }
        }

        public async Task<IEnumerable<Cor>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Cores.Where(c => c.Ativo)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Cor>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Cor>();
            }
        }
    }
}
