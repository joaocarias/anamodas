﻿using AutoMapper;
using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.Pedidos;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.BASICO + "," + Constants.LOGISTAASSOCIADO)]
    public class ClientesController : MeuController
    {
        private readonly IMapper _mapper; 
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProdutoPedidoRepositorio _produtoPedidoRepositorio;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IClienteRepositorio clienteRepositorio, IMapper mapper, ILogger<ClientesController> logger, IProdutoPedidoRepositorio produtoPedidoRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
            _logger = logger;
            _produtoPedidoRepositorio = produtoPedidoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;   
            model.Clientes = (!string.IsNullOrEmpty(model?.Filtro)) 
                ? _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepositorio.ObterTodosAsync());
           
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
                _ = Guid.TryParse(GetUserId(), out Guid userId);
                model.UsuarioCadastro = userId;

                var c = _mapper.Map<Cliente>(model);
                c = await _clienteRepositorio.AdicionarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c?.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhar(Guid guid)
        {
            try
            {
                var model = new Models.Clientes.DetalharViewModel() { Cliente = _mapper.Map<ClienteViewModel>(await _clienteRepositorio.ObterAsync(guid)) };
                model.PermitirExcluir = User.IsInRole(Constants.LOGISTAASSOCIADO) || User.IsInRole(Constants.ADMINISTRADOR);

                model.Produtos = _mapper.Map<IEnumerable<ProdutoPedidoViewModel>>(await _produtoPedidoRepositorio.ProdutosPedidoByClienteIdAsync(guid));
                model.Pedidos = model.Produtos.Select(x => x.Pedido).DistinctBy(x => x.Id).ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Authorize(Roles = Constants.ADMINISTRADOR + ","+ Constants.LOGISTAASSOCIADO)]
        public async Task<IActionResult> Excluir(Guid guid)
        {
            try
            {
                var c = await _clienteRepositorio.ObterAsync(guid);
                if(c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid });

                _ = Guid.TryParse(GetUserId(), out Guid userId);                
                c.ApagarRegistro(userId);
                await _clienteRepositorio.ApagarAsync(c);

                return RedirectToAction(nameof(Index));
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid guid)
        {
            try
            {
                SelectListEstadosBrasilViewBag();
                var model = _mapper.Map<ClienteViewModel>(await _clienteRepositorio.ObterAsync(guid));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return RedirectToAction(nameof(Detalhar), new { guid });
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
                var cliente = await _clienteRepositorio.ObterAsync(model.Id);
                if(cliente is null) return View(model);

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                var endereco = cliente.Endereco;
                if(endereco is null)
                {
                    model.Endereco.UsuarioCadastro = userId;
                    endereco = _mapper.Map<Endereco>(model.Endereco);
                }
                else
                {
                    endereco.Atualizar(model.Endereco.Logradouro, model.Endereco.Numero, model.Endereco.Bairro, model.Endereco.Complemento, model.Endereco.Cep, model.Endereco.Cidade, model.Endereco.Estado, userId);
                }

                cliente.Atualizar(
                            model.Nome,
                            model.Email,
                            model.Telefone,
                            endereco,
                            userId
                        );
                
                await _clienteRepositorio.AtualizarAsync(cliente);
                return RedirectToAction(nameof(Detalhar), new { guid = cliente.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
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
