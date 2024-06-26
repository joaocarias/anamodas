﻿using Joao.Ana.Modas.Dominio.Entidades;

namespace Joao.Ana.Modas.Dominio.IRepositorios
{
    public interface IPedidoRepositorio : IRepositorioBase<Pedido>
    {
        Task<IEnumerable<Pedido>> ObterAbertos();
    }
}
