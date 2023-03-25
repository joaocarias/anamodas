using AutoMapper;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    public class ProdutosController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly ICorRepositorio _corRepositorio;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;
        private readonly IProdutoEstoqueRepositorio _produtoEstoqueRepositorio;

        public ProdutosController(IMapper mapper, IProdutoRepositorio produtoRepositorio, IFornecedorRepositorio fornecedorRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio, IProdutoEstoqueRepositorio produtoEstoqueRepositorio)
        {
            _mapper = mapper;
            _produtoRepositorio = produtoRepositorio;
            _fornecedorRepositorio = fornecedorRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
            _produtoEstoqueRepositorio = produtoEstoqueRepositorio;
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
        public async Task<IActionResult> Novo()
        {
            await SelectListFornecedoresViewBag();
            ProdutoViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await SelectListFornecedoresViewBag(model.FornecedorId);
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
                await SelectListFornecedoresViewBag(model.FornecedorId);
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
                await SelectListFornecedoresViewBag();
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

        [HttpGet]
        public async Task<IActionResult> Incluir(Guid guid)
        {
            try
            {
                await SelectListCoresViewBag();
                await SelectListTamanhosViewBag();

                var model = new IncluirViewModel() { Produto = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(guid)) };
                model.ProdutoId = guid;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Incluir(IncluirViewModel model)
        {   
            try
            {
                if (!ModelState.IsValid)
                {
                    await SelectListTamanhosViewBag();
                    await SelectListCoresViewBag();

                    model.Produto = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(model.ProdutoId));

                    return View(model);
                }

                var produtoEstoque = await _produtoEstoqueRepositorio.GetInclusoAsync(model.ProdutoId, model.CorId, model.TamanhoId);

                if (produtoEstoque is not null)
                {
                    produtoEstoque.AtualizarEstoque(model.Quantidade);
                    await _produtoEstoqueRepositorio.AtualizarAsync(produtoEstoque);
                }
                else
                {
                    await _produtoEstoqueRepositorio.AdicionarAsync(new ProdutoEstoque(model.ProdutoId, model.CorId, model.TamanhoId, model.Quantidade));
                }

                return RedirectToAction(nameof(Detalhar), new { guid = model.ProdutoId });
            }
            catch (Exception)
            {
                await SelectListCoresViewBag();
                await SelectListTamanhosViewBag();

                model.Produto = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(model.ProdutoId));

                return View(model);
            }
        }

        #region viewBags

        private async Task SelectListTamanhosViewBag(Guid? selected = null)
        {
            ViewBag.Tamanhos = new SelectList(await _tamanhoRepositorio.ObterTodosAsync(), "Id", "Nome", selected);
        }

        private async Task SelectListCoresViewBag(Guid? selected = null)
        {
            ViewBag.Cores = new SelectList(await _corRepositorio.ObterTodosAsync(), "Id", "Nome", selected);            
        }

        private async Task SelectListFornecedoresViewBag(Guid? selected = null)
        {
            ViewBag.Fornecedores = new SelectList(await _fornecedorRepositorio.ObterTodosAsync(), "Id", "Nome", selected);
        }

        #endregion
    }
}
