using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Repositorios
{
    public class FornecedorRepositorio : IFornecedorRepositorio
    {
        private readonly AppDbContext _appDbContext;

        public FornecedorRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> AdicionarAsync(Fornecedor t)
        {
            try
            {
                await _appDbContext.Fornecedores.AddAsync(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ApagarAsync(Fornecedor t)
        {
            try
            {
                t.ApagarRegistro();
                _appDbContext.Fornecedores.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarAsync(Fornecedor t)
        {
            try
            {
                _appDbContext.Fornecedores.Update(t);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Fornecedor?> ObterAsync(Guid id)
        {
            try
            {
                var c = await _appDbContext.Fornecedores.Include(_ => _.Endereco).Where(_ => _.Ativo && _.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IList<Fornecedor>> ObterPorNomeAsync(string filtro)
        {
            try
            {
                var l = await _appDbContext.Fornecedores.Where(c => c.Ativo && c.Nome.Contains(filtro))
                                .Include(x => x.Endereco)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Fornecedor>();
            }
        }

        public async Task<IList<Fornecedor>> ObteTodosAsync()
        {
            try
            {
                var l = await _appDbContext.Fornecedores.Where(c => c.Ativo)
                                .Include(x => x.Endereco)
                                .AsNoTracking()
                                .OrderBy(c => c.Nome)
                                .ToListAsync();
                return l;
            }
            catch (Exception)
            {
                return new List<Fornecedor>();
            }
        }

        public async Task<IList<Fornecedor>> ObteTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10)
        {
            try
            {
                var l = _appDbContext.Fornecedores.Where(c => c.Ativo)
                                .Include(x => x.Endereco)
                                .OrderBy(c => c.Nome);

                return await Paginacao<Fornecedor>.CreateAsync(l.AsNoTracking(), paginaAtual ?? 1, totalPaginas);
            }
            catch (Exception)
            {
                return new List<Fornecedor>();
            }
        }
    }
}
