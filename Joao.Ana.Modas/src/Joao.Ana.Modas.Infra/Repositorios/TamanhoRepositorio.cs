using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class TamanhoRepositorio : ITamanhoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public TamanhoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(Tamanho t)
        {
            try
            {
                await _appDbContext.Tamanhos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
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
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(Tamanho t)
        {
            try
            {
                _appDbContext.Tamanhos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Tamanho?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Tamanhos.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<Tamanho>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.Tamanhos.Where(c => c.Ativo && c.Nome.Contains(filtro))                               
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Tamanho>();
            }
        }

        public async Task<IList<Tamanho>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Tamanhos.Where(c => c.Ativo)                                
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                return new List<Tamanho>();
            }
        }

        public async Task<IList<Tamanho>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Tamanhos.Where(c => c.Ativo)                                
                                .OrderBy(c => c.Nome);

                return await Paginacao<Tamanho>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<Tamanho>();
            }
        }
    }
}
