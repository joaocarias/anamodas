using AutoMapper;
using Joao.Ana.Modas.App.Records.Mensagens;
using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.IRepositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Joao.Ana.Modas.App.Controllers
{
    public class MensagemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMensagemWebSiteRepositorio _mensagemWebSiteRepositorio;

        public MensagemController(IMapper mapper, IMensagemWebSiteRepositorio mensagemWebSiteRepositorio)
        {
            _mapper = mapper;
            _mensagemWebSiteRepositorio = mensagemWebSiteRepositorio;
        }

        [HttpGet, AllowAnonymous]
        [Route("Api/[Controller]/Teste/{nome}")]
        public IActionResult Teste(string nome)
        {
            var mensagem = $"Olá, {nome}! Isso é personalizado.";
            return Ok(new { mensagem, meuTeste = "Ola Mundo Api"});
        }

        [HttpPost, AllowAnonymous]
        [Route("Api/[Controller]")]
        public async Task<IActionResult> Mensagem([FromBody][Required] MensagemRecord mensagemNova)
        {           
            if( string.IsNullOrEmpty(mensagemNova.Nome) || string.IsNullOrEmpty(mensagemNova.Email) || string.IsNullOrEmpty(mensagemNova.Telefone) || string.IsNullOrEmpty(mensagemNova.Mensagem)
                )
            {
                return BadRequest(new { statusCode = HttpStatusCode.BadRequest, mensagem = "Campo obrigatório não preenchido!", error = true });
            }

            var mensagemWebSite = new MensagemWebSite(mensagemNova.Nome, mensagemNova.Email, mensagemNova.Telefone, mensagemNova.Mensagem);
            var foiSalvo = await _mensagemWebSiteRepositorio.AdicionarAsync(mensagemWebSite);

            if(!foiSalvo)
                return BadRequest(new { statusCode = HttpStatusCode.BadRequest, mensagem = "Não foi possível salvar o cadastro!", error = true });

            return Created(mensagemWebSite.Id.ToString(), new { mensagemNova.Nome, mensagemNova.Email, mensagemNova.Mensagem, mensagemNova.Telefone });
        }
    }
}
