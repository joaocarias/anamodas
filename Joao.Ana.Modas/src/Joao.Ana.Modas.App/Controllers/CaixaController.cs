using AutoMapper;
using Joao.Ana.Modas.App.Models.Caixa;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.Dominio.Enums;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Repositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.BASICO)]
    public class CaixaController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICorRepositorio _corRepositorio;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;

        public CaixaController(IMapper mapper, IProdutoRepositorio produtoRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio)
        {
            _mapper = mapper;
            _produtoRepositorio = produtoRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Pedido(Guid? guid = null)
        {
            await ViewBagsDefault();

            if (guid is not null)
            {
                return View(new VendaViewModel()
                {
                    Id = guid.Value,
                    Status = EVendaStatus.Aberto,
                    ProdutosVenda = new List<ProdutoVendaViewModel>()
                    {
                        new (){
                            Id = guid.Value,
                            Nome = "Meu teste",
                            PrecoVenda = new decimal(45.54),
                            Quantidade = 2
                        }
                    }
                });
            }

            return View(new VendaViewModel());
        }

    [HttpPost]
    public async Task<IActionResult> Pedido(ProdutoVendaViewModel produtoVendaViewModel)
    {
        await ViewBagsDefault();

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
