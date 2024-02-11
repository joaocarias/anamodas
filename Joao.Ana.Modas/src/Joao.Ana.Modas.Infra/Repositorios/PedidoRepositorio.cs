using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public PedidoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(Pedido t)
        {
            try
            {
                await _appDbContext.Pedidos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApagarAsync(Pedido t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.Pedidos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(Pedido t)
        {
            try
            {
                _appDbContext.Pedidos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Pedido?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Pedidos.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<Pedido>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Pedidos.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.DataCadastro)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Pedido>();
            }
        }

        public async Task<IList<Pedido>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Pedidos.Where(c => c.Ativo)
                                .OrderBy(c => c.DataCadastro);

                return await Paginacao<Pedido>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<Pedido>();
            }
        }
    }
}
