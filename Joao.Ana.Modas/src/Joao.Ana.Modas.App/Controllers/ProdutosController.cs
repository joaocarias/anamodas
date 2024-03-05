using AutoMapper;
using Joao.Ana.Modas.App.Models.Pedidos;
using Joao.Ana.Modas.App.Models.Produtos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.BASICO + "," + Constants.LOGISTAASSOCIADO)]
    public class ProdutosController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly ICorRepositorio _corRepositorio;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;
        private readonly IProdutoEstoqueRepositorio _produtoEstoqueRepositorio;
        private readonly ILogistaAssociadoRepositorio _logistaAssociadoRepositorio;

        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IMapper mapper, IProdutoRepositorio produtoRepositorio, IFornecedorRepositorio fornecedorRepositorio, ICorRepositorio corRepositorio, ITamanhoRepositorio tamanhoRepositorio, IProdutoEstoqueRepositorio produtoEstoqueRepositorio, ILogistaAssociadoRepositorio logistaAssociadoRepositorio, ILogger<ProdutosController> logger)
        {
            _mapper = mapper;
            _produtoRepositorio = produtoRepositorio;
            _fornecedorRepositorio = fornecedorRepositorio;
            _corRepositorio = corRepositorio;
            _tamanhoRepositorio = tamanhoRepositorio;
            _produtoEstoqueRepositorio = produtoEstoqueRepositorio;
            _logistaAssociadoRepositorio = logistaAssociadoRepositorio;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.Produtos = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Novo()
        {
            await ViewBagsDefault();
            ProdutoViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await ViewBagsDefault(model.FornecedorId, model.LogistaAssociadoId);
                return View(model);
            }

            try
            {
                _ = Guid.TryParse(GetUserId(), out Guid userId);
                model.UsuarioCadastro = userId;

                var p = _mapper.Map<Produto>(model);
                await _produtoRepositorio.AdicionarAsync(p);
                return RedirectToAction(nameof(Detalhar), new { guid = p.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                await ViewBagsDefault(model.FornecedorId, model.LogistaAssociadoId);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhar(Guid guid)
        {
            try
            {
                var model = new Models.Produtos.DetalharViewModel() { Produto = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(guid)) };
                model.PermitirExcluir = User.IsInRole(Constants.LOGISTAASSOCIADO) || User.IsInRole(Constants.ADMINISTRADOR);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
        public async Task<IActionResult> Excluir(Guid guid)
        {
            try
            {
                var c = await _produtoRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid });

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.ApagarRegistro(userId);
                await _produtoRepositorio.ApagarAsync(c);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid guid)
        {
            try
            {                
                var model = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(guid));
                await ViewBagsDefault(model.FornecedorId, model.LogistaAssociadoId);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await ViewBagsDefault(model.FornecedorId, model.LogistaAssociadoId);
                return View(model);
            }

            try
            {                
                var p = await _produtoRepositorio.ObterSemInclusaoAsync(model.Id);
                if (p is null) return View(model);

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                p.Atualizar(model.Nome, model.PrecoUnitario, model.PrecoVenda, model.FornecedorId, model.LogistaAssociadoId, userId);
                
                await _produtoRepositorio.AtualizarAsync(p);
                return RedirectToAction(nameof(Detalhar), new { guid = p.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                await SelectListCoresViewBag();
                await SelectListTamanhosViewBag();

                model.Produto = _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterAsync(model.ProdutoId));

                return View(model);
            }
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
            return Ok(resultados);
        }

        #endregion

        #region viewBags

        private async Task ViewBagsDefault(Guid? fornecedorId = null, Guid? logistaAssociadoId = null)
        {
            await SelectListFornecedoresViewBag(fornecedorId);
            await SelectListLogistasAssociadosViewBag(logistaAssociadoId);
        }

        private async Task SelectListTamanhosViewBag(Guid? selected = null)
        {
            ViewBag.Tamanhos = new SelectList(await _tamanhoRepositorio.ObterTodosPorOrdemAsync(), "Id", "Nome", selected);
        }

        private async Task SelectListCoresViewBag(Guid? selected = null)
        {
            ViewBag.Cores = new SelectList(await _corRepositorio.ObterTodosAsync(), "Id", "Nome", selected);            
        }

        private async Task SelectListFornecedoresViewBag(Guid? selected = null)
        {
            ViewBag.Fornecedores = new SelectList(await _fornecedorRepositorio.ObterTodosAsync(), "Id", "Nome", selected);
        }

        private async Task SelectListLogistasAssociadosViewBag(Guid? selected = null)
        {
            ViewBag.LogistasAssociados = new SelectList(await _logistaAssociadoRepositorio.ObterTodosAsync(), "Id", "Nome", selected);
        }

        #endregion
    }
}
