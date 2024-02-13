using AutoMapper;
using Joao.Ana.Modas.App.Models.Caixa;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.BASICO)]
    public class CaixaController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CaixaController> _logger;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICorRepositorio _corRepositorio;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProdutoPedidoRepositorio _produtoPedidoRepositorio;       

        public CaixaController(IMapper mapper, IProdutoRepositorio produtoRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio, IPedidoRepositorio pedidoRepositorio, IClienteRepositorio clienteRepositorio, ILogger<CaixaController> logger, IProdutoPedidoRepositorio produtoPedidoRepositorio)
        {
            _mapper = mapper;
            _produtoRepositorio = produtoRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _logger = logger;
            _produtoPedidoRepositorio = produtoPedidoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Pedido(Guid? guid = null)
        {
            var pedido = guid is null ? await _pedidoRepositorio.AdicionarAsync(new Pedido()) : await _pedidoRepositorio.ObterAsync(guid.GetValueOrDefault());
            await ViewBagsDefault(pedido?.ClienteId);
            return View(_mapper.Map<PedidoViewModel>(pedido));
        }
        
        [HttpPost]
        public async Task<IActionResult> ProdutoPedido(PedidoViewModel model)
        {
            var pedido = await _pedidoRepositorio.ObterAsync(model.Id);
            var produto = await ObterProdutoOuNovo(model.Produto);
      //      Cliente? cliente = string.IsNullOrEmpty(model.Cliente?.Nome) ? null : await ObterClienteOuNovo(model.Cliente);

         //   pedido?.SetCliente(cliente?.Id);
         //   pedido = await _pedidoRepositorio.AtualizarAsync(pedido);

            await _produtoPedidoRepositorio.AdicionarAsync(new ProdutoPedido(model.Produto.Nome, model.Produto.PrecoVenda, model.Produto.Quantidade, model.Produto.CorId, model.Produto.TamanhoId, produto?.Id, pedido.Id ));
                        
            return RedirectToAction(nameof(Pedido), new { guid = pedido?.Id });
        }

        #region Endpoints

        [HttpGet]
        public async Task<IActionResult> ObterProdutos(string filtro, int? limite = null)
        {
            List<ProdutoViewModel>? lista;
            try
            {
                lista = _mapper.Map<List<ProdutoViewModel>>(await _produtoRepositorio.ObterPorNomeAsync(filtro, limite));
            }
            catch (Exception ex)
            {
                lista = Enumerable.Empty<ProdutoViewModel>().ToList();
                Console.WriteLine(ex.Message);
            }

            var resultados = lista.Select(p => new { label = p.Nome, value = p.Id });
            return Json(resultados);
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes(string filtro, int? limite = null)
        {
            List<ClienteViewModel>? lista;
            try
            {
                lista = _mapper.Map<List<ClienteViewModel>>(await _clienteRepositorio.ObterPorNomeAsync(filtro, limite));
            }
            catch (Exception ex)
            {
                lista = Enumerable.Empty<ClienteViewModel>().ToList();
                Console.WriteLine(ex.Message);
            }

            var resultados = lista.Select(p => new { label = p.Nome, value = p.Id, telefone = p.Telefone, email = p.Email });
            return Json(resultados);
        }

        #endregion

        #region privates

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
            catch(Exception ex)
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
