namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T?> ObterAsync(Guid id);
        Task<T?> AdicionarAsync(T t);
        Task<T?> AtualizarAsync(T t);
        Task<bool> ApagarAsync(T t);
        Task<IEnumerable<T>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10);
    }
}
