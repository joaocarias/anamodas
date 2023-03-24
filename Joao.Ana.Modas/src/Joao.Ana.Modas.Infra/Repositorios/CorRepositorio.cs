using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class CorRepositorio : ICorRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public CorRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(Cor t)
        {
            try
            {
                await _appDbContext.Cores.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
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
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(Cor t)
        {
            try
            {
                _appDbContext.Cores.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Cor?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Cores.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<Cor>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.Cores.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Cor>();
            }
        }

        public async Task<IList<Cor>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Cores.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Cor>();
            }
        }

        public async Task<IList<Cor>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Cores.Where(c => c.Ativo)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Cor>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<Cor>();
            }
        }
    }
}
