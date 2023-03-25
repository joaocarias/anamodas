using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class ProdutoEstoqueRepositorio : IProdutoEstoqueRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public ProdutoEstoqueRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(ProdutoEstoque t)
        {
            try
            {
                await _appDbContext.ProdutoEstoque.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApagarAsync(ProdutoEstoque t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.ProdutoEstoque.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(ProdutoEstoque t)
        {
            try
            {
                _appDbContext.ProdutoEstoque.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProdutoEstoque?> GetInclusoAsync(Guid ProdutoId, Guid CorId, Guid TamanhoId)
        {
            try
            {
                var c = await _appDbContext.ProdutoEstoque
                                                .Where(_ => _.Ativo 
                                                        && _.ProdutoId.Equals(ProdutoId)
                                                        && _.CorId.Equals(CorId)
                                                        && _.TamanhoId.Equals(TamanhoId)
                                                )                                                
                                                .AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProdutoEstoque?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.ProdutoEstoque.Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<ProdutoEstoque>> ObterTodosAsync()
        {
            try
            {
                var l = await _appDbContext.ProdutoEstoque.Where(c => c.Ativo)
                                .AsNoTracking()
                                .OrderBy(c => c.ProdutoId)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<ProdutoEstoque>();
            }
        }

        public async Task<IList<ProdutoEstoque>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.ProdutoEstoque.Where(c => c.Ativo).OrderBy(x => x.ProdutoId);
                return await Paginacao<ProdutoEstoque>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<ProdutoEstoque>();
            }
        }
    }
}
