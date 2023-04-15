using AutoMapper;
using Joao.Ana.Modas.App.Models.Tamanhos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
    public class TamanhosController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;

        public TamanhosController(IMapper mapper, ITamanhoRepositorio tamanhoRepositorio)
        {
            _mapper = mapper;
            _tamanhoRepositorio = tamanhoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.Tamanhos = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<TamanhoViewModel>>(await _tamanhoRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<TamanhoViewModel>>(await _tamanhoRepositorio.ObterTodosPorOrdemAsync());

            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new TamanhoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Novo(TamanhoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _ = Guid.TryParse(GetUserId(), out Guid userId);
                model.UsuarioCadastro = userId;

                var c = _mapper.Map<Tamanho>(model);
                await _tamanhoRepositorio.AdicionarAsync(c);
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
                return View(new DetalharViewModel() { Tamanho = _mapper.Map<TamanhoViewModel>(await _tamanhoRepositorio.ObterAsync(guid)) });
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
                var c = await _tamanhoRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid });

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.ApagarRegistro(userId);
                await _tamanhoRepositorio.ApagarAsync(c);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid guid)
        {
            try
            {
                return View(_mapper.Map<TamanhoViewModel>(await _tamanhoRepositorio.ObterAsync(guid)));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TamanhoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = await _tamanhoRepositorio.ObterAsync(model.Id);
                if (c is null) return View(model);

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.Atualizar(model.Nome, model.Ordem, userId);

                await _tamanhoRepositorio.AtualizarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}
