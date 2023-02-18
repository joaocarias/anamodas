using AutoMapper;
using Joao.Ana.Modas.Dominio.IRepositorios;

namespace Joao.Ana.Modas.App.Controllers
{
    public class ProdutoController : MeuController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(IMapper mapper, IProdutoRepositorio produtoRepositorio)
        {
            _mapper = mapper;
            _produtoRepositorio = produtoRepositorio;
        }
    }
}
