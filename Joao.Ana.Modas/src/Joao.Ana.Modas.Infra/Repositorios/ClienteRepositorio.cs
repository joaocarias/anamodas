using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
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

        public Task<bool> ApagarAsync(Cliente t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AtualizarAsync(Cliente t)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente>? ObterAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Cliente>> ObteTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Clientes.Where(c => c.Ativo)
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
    }
}
