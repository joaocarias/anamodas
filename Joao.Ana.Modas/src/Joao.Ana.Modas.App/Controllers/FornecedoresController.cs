using AutoMapper;
using Joao.Ana.Modas.App.Models.Fornecedores;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;

        public FornecedoresController(IMapper mapper, IFornecedorRepositorio fornecedorRepositorio)
        {
            _mapper = mapper;
            _fornecedorRepositorio = fornecedorRepositorio;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.Fornecedores = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<FornecedorViewModel>>(await _fornecedorRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<FornecedorViewModel>>(await _fornecedorRepositorio.ObteTodosAsync());

            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            SelectListEstadosBrasilViewBag();

            FornecedorViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(FornecedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SelectListEstadosBrasilViewBag();
                return View(model);
            }

            try
            {
                var c = _mapper.Map<Fornecedor>(model);
                await _fornecedorRepositorio.AdicionarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhar(Guid guid)
        {
            try
            {
                var model = new DetalharViewModel() { Fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterAsync(guid)) };
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
                var c = await _fornecedorRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid = guid });

                c.ApagarRegistro();
                await _fornecedorRepositorio.ApagarAsync(c);

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
                SelectListEstadosBrasilViewBag();
                var model = _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterAsync(guid));
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid = guid });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Editar(FornecedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = _mapper.Map<Fornecedor>(model);
                c.Atualizar();
                await _fornecedorRepositorio.AtualizarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        #region viewBags

        private void SelectListEstadosBrasilViewBag(string selected = "RN")
        {
            var estadosBrasil = EstadosBrasil.GetLista();
            ViewBag.EstadosBrasil = new SelectList(estadosBrasil, "Key", "Value", selected);
        }

        #endregion
    }
}
