using AutoMapper;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.Movimentacoes;
using Joao.Ana.Modas.App.Models.Pedidos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.Enums;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Joao.Ana.Modas.App.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MovimentacoesController> _logger;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICorRepositorio _corRepositorio;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProdutoPedidoRepositorio _produtoPedidoRepositorio;
        private readonly ITipoPagamentoRepositorio _tipoPagamentoRespositorio;

        public MovimentacoesController(IMapper mapper, ILogger<MovimentacoesController> logger, IProdutoRepositorio produtoRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio, IPedidoRepositorio pedidoRepositorio, IClienteRepositorio clienteRepositorio, IProdutoPedidoRepositorio produtoPedidoRepositorio, ITipoPagamentoRepositorio tipoPagamentoRespositorio)
        {
            _mapper = mapper;
            _logger = logger;
            _produtoRepositorio = produtoRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _produtoPedidoRepositorio = produtoPedidoRepositorio;
            _tipoPagamentoRespositorio = tipoPagamentoRespositorio;
        }

        [HttpGet]
        public async Task<IActionResult> ClientePedido(Guid? clienteId = null)
        {
            try
            {
                if(clienteId is null)
                {
                    await SelectListClientesViewBag();
                    return View(new ClientePedidoViewModel());
                }
                else
                {
                    var pedido = await ClienteNovoPedido(clienteId);
                    if (pedido == null) return RedirectToAction("Index", "Home");

                    return RedirectToAction(nameof(ProdutosPedido), new { pedidoId = pedido.Id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }
              
        [HttpPost]
        public async Task<IActionResult> ClientePedido(ClientePedidoViewModel model)
        {
            try
            {
                var pedido = await ClienteNovoPedido(model.ClienteId);
                if (pedido == null) return RedirectToAction("Index", "Home");

                return RedirectToAction(nameof(ProdutosPedido), new { pedidoId = pedido.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> ProdutosPedido(Guid pedidoId)
        {
            try
            {
                var t = new ProdutosPedidoViewModel()
                {
                    Pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepositorio.ObterAsync(pedidoId)),
                    Produtos = _mapper.Map<IEnumerable<ProdutoPedidoViewModel>>(await _produtoPedidoRepositorio.ProdutosPedidoAsync(pedidoId))
                };

                await ViewBagsDefault();
                return View(t);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> FormaPagamentoPedido(Guid pedidoId)
        {
            try
            {
                await SelectListTipoPagamentoViewBag();
                return View(new FormaPagamentoPedidoViewModel()
                {
                    Pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepositorio.ObterAsync(pedidoId)),
                    Produtos = _mapper.Map<IEnumerable<ProdutoPedidoViewModel>>(await _produtoPedidoRepositorio.ProdutosPedidoAsync(pedidoId))
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> FormaPagamentoPedido(FormaPagamentoPedidoViewModel model)
        {
            try
            {
                if (model?.Pedido?.TipoPagamentoId is null)
                {
                    return RedirectToAction(nameof(FormaPagamentoPedido), model.Pedido.Id);
                }

                var pedido = await _pedidoRepositorio.ObterAsync(model.Pedido.Id);
                pedido.SetTipoPagamento(model?.Pedido?.TipoPagamentoId);

                pedido = await _pedidoRepositorio.AtualizarAsync(pedido);

                return RedirectToAction(nameof(FinalizarPedido), new { pedidoId = pedido?.Id }); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> FinalizarPedido(Guid pedidoId)
        {
            try
            {
                return View(new FInalizarPedidoViewModel()
                {
                    Pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepositorio.ObterAsync(pedidoId)),
                    Produtos = _mapper.Map<IEnumerable<ProdutoPedidoViewModel>>(await _produtoPedidoRepositorio.ProdutosPedidoAsync(pedidoId))
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarPedido(FInalizarPedidoViewModel model)
        {
            try
            {
                if (model?.Pedido?.Id is not null)
                {
                    var pedido = await _pedidoRepositorio.ObterAsync(model.Pedido.Id);
                    if (pedido != null)
                    {
                        pedido.Finalizar();
                        pedido = await _pedidoRepositorio.AtualizarAsync(pedido);
                        return RedirectToAction("Detalhar", "Pedidos", new { guid = pedido?.Id });
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> NovoPedido()
        {
            try
            {
               // var pedido = guid is null ? await _pedidoRepositorio.AdicionarAsync(new Pedido()) : await _pedidoRepositorio.ObterAsync(guid.GetValueOrDefault());

                //if (pedido is not null)
                //{
                //    if (clienteId is not null)
                //    {
                //        var cliente = await _clienteRepositorio.ObterAsync(clienteId.GetValueOrDefault());
                //        pedido.SetCliente(cliente);
                //        pedido = await _pedidoRepositorio.AtualizarAsync(pedido);
                //    }
                //    else if (anonimo is not null && anonimo.GetValueOrDefault())
                //    {                       
                //        pedido.SetCliente();
                //        pedido = await _pedidoRepositorio.AtualizarAsync(pedido);
                //    }
                //}

                await ViewBagsDefault(/*pedido?.ClienteId*/);
              //  await ViewBagsDefault
                // return View(new NovoPedidoViewModel(_mapper.Map<PedidoViewModel>(pedido)));
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AdicionarProdutoPedido(AdicionarProdutoPedidoViewModel model)
        {
            try
            {
                var produtoPedido = new ProdutoPedidoViewModel()
                {
                    Nome = model.Produto.Nome,
                    PrecoVenda = model.Produto.PrecoVenda,
                    Quantidade = model.Produto.Quantidade,
                    CorId = model.Produto.CorId,
                    TamanhoId = model.Produto.TamanhoId,
                    PedidoId = model.PedidoId
                };

                if (model.Produto.ProdutoId is null)
                {
                    var produto = await ObterProdutoOuNovo(produtoPedido);
                    produtoPedido.ProdutoId = produto.Id;
                }
                else
                {
                    produtoPedido.ProdutoId = model.Produto.ProdutoId;
                }

                var newProdutoPedido = await _produtoPedidoRepositorio.AdicionarAsync(_mapper.Map<ProdutoPedido>(produtoPedido));
                return RedirectToAction(nameof(ProdutosPedido), new { pedidoId = newProdutoPedido.PedidoId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        #region privates

        private async Task<Pedido?> AtualizarStatusPedido(Guid pedidoId, EPeditoStatus status)
        {
            try
            {
                var pedido = await _pedidoRepositorio.ObterAsync(pedidoId);
                pedido?.SetStatus(status);
                return await _pedidoRepositorio.AtualizarAsync(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        private async Task<Produto?> ObterProdutoOuNovo(ProdutoPedidoViewModel produtoPedido)
        {
            try
            {
                var produto = await _produtoRepositorio.ObterAsync(produtoPedido.ProdutoId.GetValueOrDefault());
                produto ??= await _produtoRepositorio.AdicionarAsync(
                                    new Produto(
                                        produtoPedido.Nome,
                                        null,
                                        produtoPedido.PrecoVenda,
                                        Enumerable.Empty<ProdutoEstoque>(),
                                        null,
                                        null));

                return produto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        private async Task<Cliente?> ObterClienteOuNovo(ClienteViewModel model)
        {
            try
            {
                var cliente = await _clienteRepositorio.ObterAsync(model.Id);
                cliente ??= await _clienteRepositorio.AdicionarAsync(new Cliente(model.Nome, null, null, null));

                return cliente;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        private async Task<Pedido?> ClienteNovoPedido(Guid? clienteId = null)
        {
            try
            {
                var pedido = new Pedido();
                pedido.SetCliente(clienteId);
                pedido = await _pedidoRepositorio.AdicionarAsync(pedido);
                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        #endregion

        #region viewBags

        private async Task ViewBagsDefault(Guid? clienteId = null)
        {
            await SelectListProdutosViewBag();
            await SelectListTamanhosViewBag();
            await SelectListCoresViewBag();
            await SelectListClientesViewBag(clienteId);
        }

        private async Task SelectListProdutosViewBag()
        {
            ViewBag.Produtos = new SelectList(await _produtoRepositorio.ObterPorNomeAsync(), "Id", "Nome");
        }

        private async Task SelectListTamanhosViewBag(Guid? selected = null)
        {
            ViewBag.Tamanhos = new SelectList(await _tamanhoRepositorio.ObterTodosPorOrdemAsync(), "Id", "Nome", selected);
        }

        private async Task SelectListCoresViewBag(Guid? selected = null)
        {
            ViewBag.Cores = new SelectList(await _corRepositorio.ObterTodosAsync(), "Id", "Nome", selected);
        }

        private async Task SelectListClientesViewBag(Guid? selected = null)
        {
            ViewBag.Clientes = new SelectList(await _clienteRepositorio.ObterTodosAsync(), "Id", "Nome", selected);
        }

        private async Task SelectListTipoPagamentoViewBag(Guid? selected = null)
        {
            ViewBag.TipoPagamento = new SelectList(await _tipoPagamentoRespositorio.ObterTodosAsync(), "Id", "Nome", selected);
        }

        #endregion
    }
}
