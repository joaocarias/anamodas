using AutoMapper;
using Joao.Ana.Modas.App.Models.Entradas;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class EntradasController : MeuController
    {
        private readonly IMapper _mapper;

        public EntradasController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Nova()
        {
            return View(new EntradaViewModel());
        }
    }
}
