using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class TipoPagamentoRepositorio : ITipoPagamentoRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CorRepositorio> _logger;

        public TipoPagamentoRepositorio(AppDbContext appDbContext, ILogger<CorRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<TipoPagamento?> AdicionarAsync(TipoPagamento t)
        {
            try
            {
                await _appDbContext.TipoPagamentos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(TipoPagamento t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.TipoPagamentos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<TipoPagamento?> AtualizarAsync(TipoPagamento t)
        {
            try
            {
                _appDbContext.TipoPagamentos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<TipoPagamento?> ObterAsync(Guid id)
        {
            try
            {
                return await _appDbContext.TipoPagamentos.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<TipoPagamento>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                return await _appDbContext.TipoPagamentos.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .AsNoTracking()
                                .OrderBy(c => c.Ordem)
                                .OrderBy(c => c.Nome)
                                .ToListAsync();                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<TipoPagamento>();
            }
        }

        public async Task<IEnumerable<TipoPagamento>> ObterTodosAsync()
        {
            try
            {
                return await _appDbContext.TipoPagamentos.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<TipoPagamento>();
            }
        }

        public async Task<IEnumerable<TipoPagamento>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.TipoPagamentos.Where(c => c.Ativo)
                                .OrderBy(c => c.Nome);

                return await Paginacao<TipoPagamento>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<TipoPagamento>();
            }
        }

        public async Task<IEnumerable<TipoPagamento>> ObterTodosPorOrdemAsync()
        {
            try
            {
                return await _appDbContext.TipoPagamentos.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Ordem)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<TipoPagamento>();
            }
        }
    }
}
