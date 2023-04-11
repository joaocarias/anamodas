﻿using AutoMapper;
using Joao.Ana.Modas.App.Models.TipoPagamento;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR)]
    public class TipoPagamentoController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly ITipoPagamentoRepositorio _tipoPagamentoRepositorio;

        public TipoPagamentoController(IMapper mapper, ITipoPagamentoRepositorio tipoPagamentoRepositorio)
        {
            _mapper = mapper;
            _tipoPagamentoRepositorio = tipoPagamentoRepositorio;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.TipoPagamentos = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<TipoPagamentoViewModel>>(await _tipoPagamentoRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<TipoPagamentoViewModel>>(await _tipoPagamentoRepositorio.ObterTodosPorOrdemAsync());

            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new TipoPagamentoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Novo(TipoPagamentoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = _mapper.Map<TipoPagamento>(model);
                await _tipoPagamentoRepositorio.AdicionarAsync(c);
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
                return View(new DetalharViewModel() { TipoPagamento = _mapper.Map<TipoPagamentoViewModel>(await _tipoPagamentoRepositorio.ObterAsync(guid)) });
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
                var c = await _tipoPagamentoRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid });

                c.ApagarRegistro();
                await _tipoPagamentoRepositorio.ApagarAsync(c);

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
                return View(_mapper.Map<TipoPagamentoViewModel>(await _tipoPagamentoRepositorio.ObterAsync(guid)));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TipoPagamentoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = _mapper.Map<TipoPagamento>(model);
                c.Atualizar();
                await _tipoPagamentoRepositorio.AtualizarAsync(c);
                return RedirectToAction(nameof(Detalhar), new { guid = c.Id });
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}
