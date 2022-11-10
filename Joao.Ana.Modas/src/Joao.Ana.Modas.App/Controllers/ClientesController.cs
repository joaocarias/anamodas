using AutoMapper;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.Infra.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class ClientesController : MeuController
    {
        private readonly IMapper _mapper; 
        private readonly AppDbContext _appDbContext;

        public ClientesController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var lista = new List<ClienteViewModel>();

            return View(lista);
        }
    }
}
