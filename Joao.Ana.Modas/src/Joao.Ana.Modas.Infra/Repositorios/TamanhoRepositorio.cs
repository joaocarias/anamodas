using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class TamanhoRepositorio : ITamanhoRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CorRepositorio> _logger;

        public TamanhoRepositorio(AppDbContext appDbContext, ILogger<CorRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Tamanho?> AdicionarAsync(Tamanho t)
        {
            try
            {
                await _appDbContext.Tamanhos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(Tamanho t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.Tamanhos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<Tamanho?> AtualizarAsync(Tamanho t)
        {
            try
            {
                _appDbContext.Tamanhos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<Tamanho?> ObterAsync(Guid id)
        {
            try
            {
                return await _appDbContext.Tamanhos.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<Tamanho>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                return await _appDbContext.Tamanhos.Where(c => c.Ativo && c.Nome.Contains(filtro))                               
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Tamanho>();
            }
        }

        public async Task<IEnumerable<Tamanho>> ObterTodosAsync()
        {
            try
            {
                return await _appDbContext.Tamanhos.Where(c => c.Ativo)                                
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Tamanho>();
            }
        }

        public async Task<IEnumerable<Tamanho>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Tamanhos.Where(c => c.Ativo)                                
                                .OrderBy(c => c.Nome);

                return await Paginacao<Tamanho>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Tamanho>();
            }
        }

        public async Task<IEnumerable<Tamanho>> ObterTodosPorOrdemAsync()
        {
            try
            {
                return await _appDbContext.Tamanhos.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Ordem)
                                .ToListAsync();               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Tamanho>();
            }
        }
    }
}
