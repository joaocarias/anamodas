using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CorRepositorio> _logger;

        public PedidoRepositorio(AppDbContext appDbContext, ILogger<CorRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Pedido?> AdicionarAsync(Pedido t)
        {
            try
            {
                await _appDbContext.Pedidos.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
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

        public async Task<Pedido?> AtualizarAsync(Pedido t)
        {
            try
            {
                _appDbContext.Pedidos.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<Pedido?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Pedidos
                                            //  .Include(p => p.ProdutosPedido)
                                            .Include(c => c.Cliente.Endereco)
                                            .Include(c => c.TipoPagamento)
                                            .Where(_ => _.Ativo && _.Id.Equals(id))
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();
                return c;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
            try
            {
                return await _appDbContext.Pedidos
                               // .Include(p => p.ProdutosPedido)
                                .Include(c => c.Cliente.Endereco)
                                .Include(c => c.TipoPagamento)
                                .Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.DataCadastro)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Pedido>();
            }
        }

        public async Task<IEnumerable<Pedido>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Pedidos
                                .Include(c => c.Cliente.Endereco)
                                .Include(c => c.TipoPagamento)
                                .Where(c => c.Ativo)
                                .OrderBy(c => c.DataCadastro);

                return await Paginacao<Pedido>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<Pedido>();
            }
        }
    }
}
