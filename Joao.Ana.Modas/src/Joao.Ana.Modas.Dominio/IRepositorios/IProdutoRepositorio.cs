﻿using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IProdutoRepositorio : IRepositorioBase<Produto>
    {
        Task<IList<Produto>> ObterPorNomeAsync(string filtro);
        Task<Produto?> ObterSemInclusaoAsync(Guid id);
    }
}
