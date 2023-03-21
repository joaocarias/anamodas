using AutoMapper;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    public class ProdutosController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;

        public ProdutosController(IMapper mapper, IProdutoRepositorio produtoRepositorio, IFornecedorRepositorio fornecedorRepositorio)
        {
            _mapper = mapper;
            _produtoRepositorio = produtoRepositorio;
            _fornecedorRepositorio = fornecedorRepositorio;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.Produtos = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<ProdutoViewModel>>(await _produtoRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<ProdutoViewModel>>(await _produtoRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            SelectListCoresViewBag();
            SelectListTamanhosViewBag();
            SelectListFornecedoresViewBag();
            ProdutoViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
               // SelectListCoresViewBag(model.Cores);
               // SelectListTamanhosViewBag(model.Tamanho);
                SelectListFornecedoresViewBag(model.FornecedorId);
                return View(model);
            }

            try
            {
                var p = _mapper.Map<Produto>(model);
                await _produtoRepositorio.AdicionarAsync(p);
                return RedirectToAction(nameof(Detalhar), new { guid = p.Id });
            }
            catch (Exception)
            {
                //SelectListCoresViewBag(model.Cor);
                //SelectListTamanhosViewBag(model.Tamanho);
                SelectListFornecedoresViewBag(model.FornecedorId);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhar(Guid guid)
        {
            try
            {
                var model = new DetalharViewModel() { Produto = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(guid)) };
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(Guid guid)
        {
            try
            {
                var c = await _produtoRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid = guid });

                c.ApagarRegistro();
                await _produtoRepositorio.ApagarAsync(c);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid = guid });
            }
        }

        public async Task<IActionResult> Editar(Guid guid)
        {
            try
            {
                SelectListCoresViewBag();
                SelectListTamanhosViewBag();
                SelectListFornecedoresViewBag();
                var model = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(guid));
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid = guid });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Editar(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var p = _mapper.Map<Produto>(model);
                p.Atualizar();
                await _produtoRepositorio.AtualizarAsync(p);
                return RedirectToAction(nameof(Detalhar), new { guid = p.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }


        #region viewBags

        private void SelectListTamanhosViewBag(string? selected = null)
        {
            ViewBag.Tamanhos = new SelectList(Tamanhos.GetLista(), "Key", "Value", selected);
        }

        private void SelectListCoresViewBag(string? selected = null)
        {
            ViewBag.Cores = new SelectList(Cores.GetLista(), "Key", "Value", selected);            
        }

        private async void SelectListFornecedoresViewBag(Guid? selected = null)
        {
            var lista = await _fornecedorRepositorio.ObterTodosAsync();
            ViewBag.Fornecedores = new SelectList(lista, "Id", "Nome", selected);
        }

        #endregion
    }
}
