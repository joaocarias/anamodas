using AutoMapper;
using Joao.Ana.Modas.App.Models.Tamanhos;
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
                : _mapper.Map<IList<TamanhoViewModel>>(await _tamanhoRepositorio.ObterTodosAsync());

            return View(model);
        }
    }
}
