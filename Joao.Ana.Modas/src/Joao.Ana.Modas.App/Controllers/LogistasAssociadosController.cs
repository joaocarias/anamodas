using AutoMapper;
using Joao.Ana.Modas.App.Models.LogistaAssociado;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR)]
    public class LogistasAssociadosController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ILogistaAssociadoRepositorio _logistaAssociadoRepositorio;

        public LogistasAssociadosController(IMapper mapper, ILogistaAssociadoRepositorio logistaAssociadoRepositorio)
        {
            _mapper = mapper;
            _logistaAssociadoRepositorio = logistaAssociadoRepositorio;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.LogistasAssociados = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<LogistaAssociadoViewModel>>(await _logistaAssociadoRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<LogistaAssociadoViewModel>>(await _logistaAssociadoRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new LogistaAssociadoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Novo(LogistaAssociadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = _mapper.Map<LogistaAssociado>(model);
                await _logistaAssociadoRepositorio.AdicionarAsync(c);
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
                return View(new DetalharViewModel() { LogistaAssociado = _mapper.Map<LogistaAssociadoViewModel>(await _logistaAssociadoRepositorio.ObterAsync(guid)) });
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
                var c = await _logistaAssociadoRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid });

                c.ApagarRegistro();
                await _logistaAssociadoRepositorio.ApagarAsync(c);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        public async Task<IActionResult> Editar(Guid guid)
        {
            try
            {
                return View(_mapper.Map<LogistaAssociadoViewModel>(await _logistaAssociadoRepositorio.ObterAsync(guid)));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(LogistaAssociadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = _mapper.Map<LogistaAssociado>(model);
                c.Atualizar();
                await _logistaAssociadoRepositorio.AtualizarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}
