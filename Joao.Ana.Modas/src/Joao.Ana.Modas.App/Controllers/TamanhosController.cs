using AutoMapper;
using Joao.Ana.Modas.App.Models.Tamanhos;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class TamanhosController : MeuController
    {
        private readonly IMapper _mapper;

        public TamanhosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index(IndexViewModel model)
        {
            //model = model is null ? new IndexViewModel() : model;
            //model.Tamanhos = (!string.IsNullOrEmpty(model?.Filtro))
            //    ? _mapper.Map<IList<IndexViewModel>>(await _fornecedorRepositorio.ObterPorNomeAsync(model.Filtro))
            //    : _mapper.Map<IList<IndexViewModel>>(await _fornecedorRepositorio.ObterTodosAsync());

            return View(model);
        }
    }
}
