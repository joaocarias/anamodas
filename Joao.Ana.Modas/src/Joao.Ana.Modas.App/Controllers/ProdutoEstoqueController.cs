using AutoMapper;
using Joao.Ana.Modas.App.Models.ProdutoEstoques;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.BASICO + "," + Constants.LOGISTAASSOCIADO)]
    public class ProdutoEstoqueController : MeuController
    {
        private readonly IMapper _mapper;

        private readonly IProdutoEstoqueRepositorio _produtoEstoqueRepositorio;
        private readonly ICorRepositorio _corRepositorio;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;

        public ProdutoEstoqueController(IMapper mapper, IProdutoEstoqueRepositorio produtoEstoqueRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio)
        {
            _mapper = mapper;
            _produtoEstoqueRepositorio = produtoEstoqueRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.ProdutoEstoques = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<ProdutoEstoqueViewModel>>(await _produtoEstoqueRepositorio.ObterPorFiltro(model.Filtro))
                : _mapper.Map<IList<ProdutoEstoqueViewModel>>(await _produtoEstoqueRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
        public async Task<IActionResult> CheckEstoque(CheckEstoqueViewModel model)
        {
            model = model is null ? new CheckEstoqueViewModel() : model;
            model.ProdutoEstoques = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<ProdutoEstoqueViewModel>>(await _produtoEstoqueRepositorio.ObterPorFiltro(model.Filtro))
                : _mapper.Map<IList<ProdutoEstoqueViewModel>>(await _produtoEstoqueRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
        public async Task<IActionResult> Check(Guid guid)
        {
            try
            {
                await SelectListCoresViewBag();
                await SelectListTamanhosViewBag();

                var model = _mapper.Map<ItemCheckViewModel>(await _produtoEstoqueRepositorio.ObterAsync(guid));
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
        public async Task<IActionResult> Check(ItemCheckViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await SelectListTamanhosViewBag();
                    await SelectListCoresViewBag();
                    
                    return View(model);
                }

                var pe = await _produtoEstoqueRepositorio.ObterAsync(model.Id);
                if (pe == null) return View(model);

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                pe.AtualizarChecking(model.Check, userId);

                await _produtoEstoqueRepositorio.AtualizarAsync(pe);

                return RedirectToAction(nameof(CheckEstoque));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

            #region viewBags


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
