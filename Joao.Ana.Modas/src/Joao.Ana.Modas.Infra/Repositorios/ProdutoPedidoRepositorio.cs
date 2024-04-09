using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class ProdutoPedidoRepositorio : IProdutoPedidoRepositorio
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ProdutoPedidoRepositorio> _logger;

        public ProdutoPedidoRepositorio(AppDbContext appDbContext, ILogger<ProdutoPedidoRepositorio> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<ProdutoPedido?> AdicionarAsync(ProdutoPedido t)
        {
            try
            {
                await _appDbContext.ProdutosPedido.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> ApagarAsync(ProdutoPedido t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.ProdutosPedido.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<ProdutoPedido?> AtualizarAsync(ProdutoPedido t)
        {
            try
            {
                _appDbContext.ProdutosPedido.Update(t);
                await _appDbContext.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<ProdutoPedido?> ObterAsync(Guid id)
        {
            try
            {
                return await _appDbContext.ProdutosPedido
                                    .Include(p => p.Produto)
                                    .Include(x => x.Pedido)
                                    .Include(x => x.Cor)
                                    .Include(x => x.Tamanho)
                                    .Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<ProdutoPedido>> ObterTodosAsync()
        {
            try
            {
                return await _appDbContext.ProdutosPedido
                                .Include(p => p.Produto)
                                .Include(x => x.Pedido)
                                .Include(x => x.Cor)
                                .Include(x => x.Tamanho)
                                .Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.DataCadastro)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ProdutoPedido>();
            }
        }

        public async Task<IEnumerable<ProdutoPedido>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.ProdutosPedido
                                .Include(p => p.Produto)
                                .Include(x => x.Pedido)
                                .Include(x => x.Cor)
                                .Include(x => x.Tamanho)
                                .Where(c => c.Ativo)
                                .OrderBy(c => c.DataCadastro);

                return await Paginacao<ProdutoPedido>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ProdutoPedido>();
            }
        }

        public async Task<IEnumerable<ProdutoPedido>> ProdutosPedidoAsync(Guid pedidoId)
        {
            try
            {
                return await _appDbContext.ProdutosPedido
                                .Include(p => p.Pedido)
                                .Include(p => p.Produto)
                                .Include(x => x.Cor)
                                .Include(x => x.Tamanho)
                                .AsNoTracking()
                                .Where(c => c.Ativo && c.PedidoId.Equals(pedidoId))
                                .OrderBy(c => c.DataCadastro)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ProdutoPedido>();
            }
        }

        public async Task<IEnumerable<ProdutoPedido>> ProdutosPedidoByClienteIdAsync(Guid clienteId)
        {
            try
            {
                return await _appDbContext.ProdutosPedido
                                .Include(p => p.Pedido)
                                .Include(p => p.Produto)
                                .Include(x => x.Cor)
                                .Include(x => x.Tamanho)
                                .AsNoTracking()
                                .Where(c => c.Ativo && c.Pedido != null && c.Pedido.ClienteId != null && c.Pedido.ClienteId.Equals(clienteId))
                                .OrderBy(c => c.DataCadastro)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Enumerable.Empty<ProdutoPedido>();
            }
        }

        public async Task<decimal> ValorTotalPedido(Guid pedidoId)
        {
            try
            {
                var total = await _appDbContext.ProdutosPedido
                               .Where(x => x.Ativo && x.PedidoId == pedidoId)
                               .AsNoTracking()
                               .SumAsync(x => x.PrecoVenda);
                return total is not null && total >= 0 ? total.Value : 0;  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return 0;
            }
        }
    }
}
