using AutoMapper;
using Joao.Ana.Modas.App.Models.Cores;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
    public class CoresController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ICorRepositorio _corRepositorio;

        public CoresController(IMapper mapper, ICorRepositorio corRepositorio)
        {
            _mapper = mapper;
            _corRepositorio = corRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.Cores = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<CorViewModel>>(await _corRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<CorViewModel>>(await _corRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new CorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Novo(CorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _ = Guid.TryParse(GetUserId(), out Guid userId);
                model.UsuarioCadastro = userId;

                var c = _mapper.Map<Cor>(model);
                await _corRepositorio.AdicionarAsync(c);
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
                return View(new DetalharViewModel() { Cor = _mapper.Map<CorViewModel>(await _corRepositorio.ObterAsync(guid)) });
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
                var c = await _corRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid });

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.ApagarRegistro(userId);
                await _corRepositorio.ApagarAsync(c);

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
                return View(_mapper.Map<CorViewModel>(await _corRepositorio.ObterAsync(guid)));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(CorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = await _corRepositorio.ObterAsync(model.Id);
                if (c is null) return View(model);

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.Atualizar(model.Nome, userId);

                await _corRepositorio.AtualizarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}
