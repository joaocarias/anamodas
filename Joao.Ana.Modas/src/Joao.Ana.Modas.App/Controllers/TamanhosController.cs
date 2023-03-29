using AutoMapper;
using Joao.Ana.Modas.App.Models.Tamanhos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class TamanhosController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ITamanhoRepositorio _tamanhoRepositorio;

        public TamanhosController(IMapper mapper, ITamanhoRepositorio tamanhoRepositorio)
        {
            _mapper = mapper;
            _tamanhoRepositorio = tamanhoRepositorio;
        }

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

                c.ApagarRegistro();
                await _tamanhoRepositorio.ApagarAsync(c);

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
                var c = _mapper.Map<Tamanho>(model);
                c.Atualizar();
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
