using AutoMapper;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class ClientesController : MeuController
    {
        private readonly IMapper _mapper; 
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClientesController(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lista = _mapper.Map<IList<ClienteViewModel>>(await _clienteRepositorio.ObteTodosAsync());
            return View(lista);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            ClienteViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var c = _mapper.Map<Cliente>(model);
            await _clienteRepositorio.AdicionarAsync(c);

            return RedirectToAction(nameof(Index));
        }
    }
}
