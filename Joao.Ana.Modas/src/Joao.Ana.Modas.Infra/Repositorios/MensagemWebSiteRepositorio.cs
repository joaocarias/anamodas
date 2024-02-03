using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class MensagemWebSiteRepositorio : IMensagemWebSiteRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public MensagemWebSiteRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(MensagemWebSite t)
        {
            try
            {
                await _appDbContext.MensagensWebSites.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApagarAsync(MensagemWebSite t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.MensagensWebSites.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(MensagemWebSite t)
        {
            try
            {
                _appDbContext.MensagensWebSites.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<MensagemWebSite?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.MensagensWebSites.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<MensagemWebSite>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.MensagensWebSites.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<MensagemWebSite>();
            }
        }

        public async Task<IList<MensagemWebSite>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.MensagensWebSites.Where(c => c.Ativo)
                                .OrderBy(c => c.Nome);

                return await Paginacao<MensagemWebSite>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<MensagemWebSite>();
            }
        }
    }
}
