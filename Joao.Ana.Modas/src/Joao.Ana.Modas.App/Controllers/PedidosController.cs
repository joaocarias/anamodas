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
    public class PedidosController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PedidosController> _logger;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IProdutoPedidoRepositorio _produtoPedidoRepositorio;

        public PedidosController(IMapper mapper, ILogger<PedidosController> logger, IPedidoRepositorio pedidoRepositorio, IProdutoPedidoRepositorio produtoPedidoRepositorio)
        {
            _mapper = mapper;
            _logger = logger;
            _pedidoRepositorio = pedidoRepositorio;
            _produtoPedidoRepositorio = produtoPedidoRepositorio;
        }

        public async Task<IActionResult> Detalhar(Guid guid)
        {
            try
            {             
                return View(new DetalharViewModel() {
                    Pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepositorio.ObterAsync(guid)) ,
                    Produtos = _mapper.Map<IEnumerable<ProdutoPedidoViewModel>>(await _produtoPedidoRepositorio.ProdutosPedidoAsync(guid))       
                });
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
                    pedido.Cancelar();
                    pedido = await _pedidoRepositorio.AtualizarAsync(pedido);
                    return RedirectToAction(nameof(Detalhar), new { guid });
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
