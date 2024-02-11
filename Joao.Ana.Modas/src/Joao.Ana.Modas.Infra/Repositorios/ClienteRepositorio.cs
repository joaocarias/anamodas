using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ClienteRepositorio> _logger;

        public ClienteRepositorio(AppDbContext appDbContext, ILogger<ClienteRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Cliente?> AdicionarAsync(Cliente t)
        {
            try
            {
                await _appDbContext.Clientes.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t; 
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(Cliente t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.Clientes.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Cliente?> AtualizarAsync(Cliente t)
        {
            try
            {
                _appDbContext.Clientes.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Cliente?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Clientes.Include(_ => _.Endereco).Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Cliente>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.Clientes.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .Include(x => x.Endereco)
                                .AsNoTracking()                                
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Cliente>();
            }
        }

        public async Task<IEnumerable<Cliente>> ObterPorNomePaginadoAsync(string filtro, int? paginaAtual)
        {
            try
            {
                var totalPaginas = 3;
                var l = _appDbContext.Clientes.Where(c => c.Ativo)
                                .Include(x => x.Endereco)
                                .Where(x => x.Nome.Contains(filtro) && x.Ativo)
                                .OrderBy(c => c.Nome);
                               
                return await Paginacao<Cliente>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Cliente>();
            }
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Clientes.Where(c => c.Ativo)
                                .Include(x => x.Endereco)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Cliente>();
            }
        }

        public async Task<IEnumerable<Cliente>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Clientes.Where(c => c.Ativo)
                                .Include(x => x.Endereco)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Cliente>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<Cliente>();
            }
        }
    }
}
