using AutoMapper;
using Joao.Ana.Modas.App.Models.Pedidos;
using Joao.Ana.Modas.Dominio.Enums;
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
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public PedidoController(IMapper mapper, ILogger<PedidoController> logger, IPedidoRepositorio pedidoRepositorio)
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

        public async Task<IActionResult> Cancelar(Guid guid)
        {
            try
            {
                var pedido = await _pedidoRepositorio.ObterAsync(guid);
                if(pedido is not null)
                {
                    pedido.SetStatus(EPeditoStatus.Cancelado);
                    pedido.Atualizar();

                    pedido = await _pedidoRepositorio.AtualizarAsync(pedido);
                    return RedirectToAction(nameof(Detalhar), pedido?.Id);
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return View();
            }
        }

        public async Task<IActionResult> Novo(Guid? clienteId = null)
        {
            try
            {


                return View();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);  
                return View();
            }
        }
    }
}
