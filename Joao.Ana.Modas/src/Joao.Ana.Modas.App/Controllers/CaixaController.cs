using AutoMapper;
using Joao.Ana.Modas.App.Models.Caixa;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.BASICO)]
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
