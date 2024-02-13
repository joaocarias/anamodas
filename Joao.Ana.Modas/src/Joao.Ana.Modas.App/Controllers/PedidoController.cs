using AutoMapper;
using Joao.Ana.Modas.App.Models.Caixa;
using Joao.Ana.Modas.App.Models.Pedidos;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
    public class PedidoController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ClientesController> _logger;
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public PedidoController(IMapper mapper, ILogger<ClientesController> logger, IPedidoRepositorio pedidoRepositorio)
        {
            _mapper = mapper;
            _logger = logger;
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task<IActionResult> Detalhar(Guid guid)
        {
            try
            {
                return View(new DetalharViewModel() { Pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepositorio.ObterAsync(guid))});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View();
            }
        }
    }
}
