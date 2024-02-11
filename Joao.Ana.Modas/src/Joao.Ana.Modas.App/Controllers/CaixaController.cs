using AutoMapper;
using Joao.Ana.Modas.App.Models.Caixa;
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
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICorRepositorio _corRepositorio;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public CaixaController(IMapper mapper, IProdutoRepositorio produtoRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio, IPedidoRepositorio pedidoRepositorio)
        {
            _mapper = mapper;
            _produtoRepositorio = produtoRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Pedido(Guid? guid = null)
        {
            await ViewBagsDefault();

            if (guid is not null)
            {
                var novoPedido = new Pedido();
                await _pedidoRepositorio.AdicionarAsync(novoPedido);
                return View(_mapper.Map<PedidoViewModel>(novoPedido));
            }

            return View(_mapper.Map<PedidoViewModel>(await _pedidoRepositorio.ObterAsync(guid.GetValueOrDefault())));
        }

        [HttpPost]
        public async Task<IActionResult> ProdutoPedido(ProdutoPedidoViewModel produtoPedido)
        {
            await ViewBagsDefault();

            var pedido = await _pedidoRepositorio.ObterAsync(produtoPedido.PedidoId);
            var produto = await _produtoRepositorio.ObterAsync(produtoPedido.Id);
            if(produto is null)
            {
                await _produtoRepositorio.AdicionarAsync(
                    new Produto(
                        produtoPedido.Nome,
                        null,
                        produtoPedido.PrecoVenda,
                        Enumerable.Empty<ProdutoEstoque>(),
                        null,
                        null)
                    );
            }

            produto = await _produtoRepositorio.ObterAsync(produtoPedido.Id);




            return RedirectToAction(nameof(Pedido), new { guid = Guid.NewGuid() });
        }

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

        #region viewBags

        private async Task ViewBagsDefault()
        {
            await SelectListProdutosViewBag();
            await SelectListTamanhosViewBag();
            await SelectListCoresViewBag();
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

        #endregion
    }
}
