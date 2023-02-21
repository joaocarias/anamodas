using AutoMapper;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;   
            model.Clientes = (!string.IsNullOrEmpty(model?.Filtro)) 
                ? _mapper.Map<IList<ClienteViewModel>>(await _clienteRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<ClienteViewModel>>(await _clienteRepositorio.ObterTodosAsync());
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            SelectListEstadosBrasilViewBag();

            ClienteViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SelectListEstadosBrasilViewBag();
                return View(model);
            }

            try
            {
                var c = _mapper.Map<Cliente>(model);
                await _clienteRepositorio.AdicionarAsync(c);
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
                var model = new DetalharViewModel() { Cliente = _mapper.Map<ClienteViewModel>(await _clienteRepositorio.ObterAsync(guid)) };
                return View(model);
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
                var c = await _clienteRepositorio.ObterAsync(guid);
                if(c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid = guid });

                c.ApagarRegistro();
                await _clienteRepositorio.ApagarAsync(c);

                return RedirectToAction(nameof(Index));
            }catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid = guid });
            }
        }

        public async Task<IActionResult> Editar(Guid guid)
        {
            try
            {
                SelectListEstadosBrasilViewBag();
                var model = _mapper.Map<ClienteViewModel>(await _clienteRepositorio.ObterAsync(guid));
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid = guid });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Editar(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = _mapper.Map<Cliente>(model);
                c.Atualizar();
                await _clienteRepositorio.AtualizarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        #region viewBags

        private void SelectListEstadosBrasilViewBag(string selected = "RN")
        {
            ViewBag.EstadosBrasil  = new SelectList(EstadosBrasil.GetLista(), "Key", "Value", selected);
        }

        #endregion
    }
}
