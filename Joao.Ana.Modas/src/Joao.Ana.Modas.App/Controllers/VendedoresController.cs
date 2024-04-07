using AutoMapper;
using Joao.Ana.Modas.App.Models.Vendedores;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.Enums;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Joao.Ana.Modas.Dominio.Extensoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.BASICO + "," + Constants.LOGISTAASSOCIADO)]
    public class VendedoresController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<VendedoresController> _logger;
        private readonly IVendedorRepositorio _vendedorRepositorio;

        public VendedoresController(IMapper mapper, ILogger<VendedoresController> logger, IVendedorRepositorio vendedorRepositorio)
        {
            _mapper = mapper;
            _logger = logger;
            _vendedorRepositorio = vendedorRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.Vendedores = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IEnumerable<VendedorViewModel>>(await _vendedorRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IEnumerable<VendedorViewModel>>(await _vendedorRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Novo()
        {
            SelectListTipoComissaoViewBag();
            return View(new VendedorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Novo(VendedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SelectListTipoComissaoViewBag();
                return View(model);
            }

            try
            {
                _ = Guid.TryParse(GetUserId(), out Guid userId);
                model.UsuarioCadastro = userId;

                var c = _mapper.Map<Vendedor>(model);
                await _vendedorRepositorio.AdicionarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhar(Guid guid)
        {
            try
            {
                return View(new DetalharViewModel() { Vendedor = _mapper.Map<VendedorViewModel>(await _vendedorRepositorio.ObterAsync(guid)) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(Guid guid)
        {
            try
            {
                var c = await _vendedorRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid });

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.ApagarRegistro(userId);
                await _vendedorRepositorio.ApagarAsync(c);

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
                return View(_mapper.Map<VendedorViewModel>(await _vendedorRepositorio.ObterAsync(guid)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(VendedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = await _vendedorRepositorio.ObterAsync(model.Id);
                if (c is null) return View(model);

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.Atualizar(model.Nome, model.Email, model.Telefone, model.TipoComissao, model.Comissao, userId);
                await _vendedorRepositorio.AtualizarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View(model);
            }
        }

        #region viewBags

        private void SelectListTipoComissaoViewBag(Guid? selected = null)
        {
            var selectListItems = new List<SelectListItem>() {
                new ()
                {
                    Value = ETipoComissao.Porcentagem.ToString(),
                    Text = ETipoComissao.Porcentagem.ToStringParse()
                },
                new ()
                {
                    Value = ETipoComissao.ValorFixo.ToString(),
                    Text = ETipoComissao.ValorFixo.ToStringParse()
                }
            };

            ViewBag.TipoComissao = new SelectList(selectListItems, "Value", "Text", selected);
        }

        #endregion
    }
}
