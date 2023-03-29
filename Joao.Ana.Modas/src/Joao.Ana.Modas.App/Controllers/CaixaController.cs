using AutoMapper;
using Joao.Ana.Modas.App.Models.Caixa;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class CaixaController : MeuController
    {
        private readonly IMapper _mapper;

        public CaixaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Pedido()
        {
            return View(new PedidoViewModel());
        }
    }
}
