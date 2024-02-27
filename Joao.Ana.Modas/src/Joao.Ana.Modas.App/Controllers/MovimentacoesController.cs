using AutoMapper;
using Joao.Ana.Modas.App.Models.Caixa;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.Movimentacoes;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.Enums;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public MovimentacoesController(IMapper mapper, ILogger<MovimentacoesController> logger, IProdutoRepositorio produtoRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio, IPedidoRepositorio pedidoRepositorio, IClienteRepositorio clienteRepositorio, IProdutoPedidoRepositorio produtoPedidoRepositorio)
        {
            _mapper = mapper;
            _logger = logger;
            _produtoRepositorio = produtoRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _produtoPedidoRepositorio = produtoPedidoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarPedido(Guid? guid = null)
        {
            try
            {
                var pedido = guid is null ? await _pedidoRepositorio.AdicionarAsync(new Pedido()) : await _pedidoRepositorio.ObterAsync(guid.GetValueOrDefault());
                await ViewBagsDefault(/*pedido?.ClienteId*/);
                var viewModel = new RegistrarPedidoViewModel(_mapper.Map<PedidoViewModel>(pedido));  
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction("Index", "Home");
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> ProdutoPedido(RegistrarPedidoViewModel model)
        {
            var pedido = await _pedidoRepositorio.ObterAsync(model.PedidoId.GetValueOrDefault());
            var produto = await ObterProdutoOuNovo(model.Produto);
            
            await _produtoPedidoRepositorio.AdicionarAsync(new ProdutoPedido(model.Produto.Nome, model.Produto.PrecoVenda, model.Produto.Quantidade, model.Produto.CorId, model.Produto.TamanhoId, produto?.Id, pedido.Id));

            return RedirectToAction(nameof(RegistrarPedido), new { guid = pedido?.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarPedido(FinalizarPedidoViewModel confirmar)
        {
            var pedido = await AtualizarStatusPedido(confirmar.PedidoId, EPeditoStatus.Finalizado);
            return RedirectToAction();
        }

        [HttpPost]
        public async Task<IActionResult> CancelarPedido(FinalizarPedidoViewModel confirmar)
        {
            var pedido = await AtualizarStatusPedido(confirmar.PedidoId, EPeditoStatus.Cancelado);
            return RedirectToAction("Detalhar", "Pedido", pedido.Id);
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
                var produto = await _produtoRepositorio.ObterAsync(produtoPedido.ProdutoId);
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

        #endregion
    }
}
