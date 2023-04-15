using AutoMapper;
using Joao.Ana.Modas.App.Models.Fornecedores;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR + "," + Constants.LOGISTAASSOCIADO)]
    public class FornecedoresController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;

        public FornecedoresController(IMapper mapper, IFornecedorRepositorio fornecedorRepositorio)
        {
            _mapper = mapper;
            _fornecedorRepositorio = fornecedorRepositorio;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model = model is null ? new IndexViewModel() : model;
            model.Fornecedores = (!string.IsNullOrEmpty(model?.Filtro))
                ? _mapper.Map<IList<FornecedorViewModel>>(await _fornecedorRepositorio.ObterPorNomeAsync(model.Filtro))
                : _mapper.Map<IList<FornecedorViewModel>>(await _fornecedorRepositorio.ObterTodosAsync());

            return View(model);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            SelectListEstadosBrasilViewBag();

            FornecedorViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Novo(FornecedorViewModel model)
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

                var c = _mapper.Map<Fornecedor>(model);
                await _fornecedorRepositorio.AdicionarAsync(c);
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
                var model = new DetalharViewModel() { Fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterAsync(guid)) };
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
                var c = await _fornecedorRepositorio.ObterAsync(guid);
                if (c is null)
                    return RedirectToAction(nameof(Detalhar), new { guid = guid });

                _ = Guid.TryParse(GetUserId(), out Guid userId);
                c.ApagarRegistro(userId);
                await _fornecedorRepositorio.ApagarAsync(c);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid = guid });
            }
        }

        public async Task<IActionResult> Editar(Guid guid)
        {
            try
            {
                SelectListEstadosBrasilViewBag();
                var model = _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterAsync(guid));
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Detalhar), new { guid = guid });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(FornecedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var c = await _fornecedorRepositorio.ObterAsync(model.Id);
                if (c is null) return View(model);

                _ = Guid.TryParse(GetUserId(), out Guid userId);

                var endereco = c.Endereco;
                if (endereco is null)
                {
                    model.Endereco.UsuarioCadastro = userId;
                    endereco = _mapper.Map<Endereco>(model.Endereco);
                }
                else
                {
                    endereco.Atualizar(model.Endereco.Logradouro, model.Endereco.Numero, model.Endereco.Bairro, model.Endereco.Complemento, model.Endereco.Cep, model.Endereco.Cidade, model.Endereco.Estado, userId);
                }

                c.Atualizar(model.Nome, model.Email, model.Telefone, endereco, userId);
                               
                await _fornecedorRepositorio.AtualizarAsync(c);
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
            ViewBag.EstadosBrasil = new SelectList(EstadosBrasil.GetLista(), "Key", "Value", selected);
        }

        #endregion
    }
}
