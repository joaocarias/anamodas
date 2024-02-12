using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class LogistaAssociadoRepositorio : ILogistaAssociadoRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<LogistaAssociadoRepositorio> _logger;

        public LogistaAssociadoRepositorio(AppDbContext appDbContext, ILogger<LogistaAssociadoRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<LogistaAssociado?> AdicionarAsync(LogistaAssociado t)
        {
            try
            {
                await _appDbContext.LogistasAssociados.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(LogistaAssociado t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.LogistasAssociados.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<LogistaAssociado?> AtualizarAsync(LogistaAssociado t)
        {
            try
            {
                _appDbContext.LogistasAssociados.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<LogistaAssociado?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.LogistasAssociados.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<LogistaAssociado>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.LogistasAssociados.Where(c => c.Ativo && c.Nome.Contains(filtro))                                
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<LogistaAssociado>();
            }
        }

        public async Task<IEnumerable<LogistaAssociado>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.LogistasAssociados.Where(c => c.Ativo)                               
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<LogistaAssociado>();
            }
        }

        public async Task<IEnumerable<LogistaAssociado>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.LogistasAssociados.Where(c => c.Ativo)                                
                                .OrderBy(c => c.Nome);

                return await Paginacao<LogistaAssociado>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<LogistaAssociado>();
            }
        }
    }
}
