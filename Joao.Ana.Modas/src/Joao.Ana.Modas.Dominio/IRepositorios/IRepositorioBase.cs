namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<IList<T>> ObterTodosAsync();
        Task<T?> ObterAsync(Guid id);
        Task<bool> AdicionarAsync(T t);
        Task<bool> AtualizarAsync(T t);
        Task<bool> ApagarAsync(T t);
        Task<IList<T>> ObterTodosPaginadoAsync(int? paginaAtual, int totalPaginas = 10);
    }
}
