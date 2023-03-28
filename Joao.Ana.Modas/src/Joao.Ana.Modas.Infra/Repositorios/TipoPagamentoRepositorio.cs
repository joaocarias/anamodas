using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class TipoPagamentoRepositorio : ITipoPagamentoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public TipoPagamentoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(TipoPagamento t)
        {
            try
            {
                await _appDbContext.TipoPagamentos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
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
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(TipoPagamento t)
        {
            try
            {
                _appDbContext.TipoPagamentos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<TipoPagamento?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.TipoPagamentos.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<TipoPagamento>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.TipoPagamentos.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .AsNoTracking()
                                .OrderBy(c => c.Ordem)
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<TipoPagamento>();
            }
        }

        public async Task<IList<TipoPagamento>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.TipoPagamentos.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                return new List<TipoPagamento>();
            }
        }

        public async Task<IList<TipoPagamento>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.TipoPagamentos.Where(c => c.Ativo)
                                .OrderBy(c => c.Nome);

                return await Paginacao<TipoPagamento>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<TipoPagamento>();
            }
        }

        public async Task<IList<TipoPagamento>> ObterTodosPorOrdemAsync()
        {
            try
            {
                var l = await _appDbContext.TipoPagamentos.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.Ordem)
                                .ToListAsync();
                return l;
            }
            catch (Exception ex)
            {
                return new List<TipoPagamento>();
            }
        }
    }
}
