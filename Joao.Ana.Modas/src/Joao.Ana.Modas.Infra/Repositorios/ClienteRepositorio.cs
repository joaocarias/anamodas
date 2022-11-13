using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public ClienteRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(Cliente t)
        {
            try
            {
                await _appDbContext.Clientes.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true; 
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApagarAsync(Cliente t)
        {
            try
            {
                _appDbContext.Clientes.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(Cliente t)
        {
            try
            {
                _appDbContext.Clientes.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Cliente>? ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Clientes.Include(_ => _.Endereco).Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<Cliente>> ObterPorNomeAsync(string filtro)
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
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }

        public async Task<IList<Cliente>> ObterPorNomePaginadoAsync(string filtro, int? paginaAtual)
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
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }

        public async Task<IList<Cliente>> ObteTodosAsync()
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
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }

        public async Task<IList<Cliente>> ObteTodosPaginadoAsync(int? paginaAtual)
        {
            try
            {
                var totalPaginas = 3;
                var l = _appDbContext.Clientes.Where(c => c.Ativo)
                                .Include(x => x.Endereco)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Cliente>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<Cliente>();
            }
        }
    }
}
