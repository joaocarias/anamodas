using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.Dominio.Enums;

namespace Joao.Ana.Modas.App.Models.Pedidos
{
    public class PedidoViewModel : EntidadeBaseViewModel
    {
        public Guid? ClienteId { get; set; }

        public ClienteViewModel? Cliente { get; set; }

        public bool ClienteAnonimo { get; set; } = false;

        //  public IEnumerable<ProdutoPedidoViewModel>? ProdutosPedido { get; set; }

        public EPeditoStatus Status { get; set; }
    }
}
