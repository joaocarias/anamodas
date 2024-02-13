using Joao.Ana.Modas.Dominio.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Pedido : EntidadeBase
    {
        public EPeditoStatus Status { get; private set; }

        public Guid? ClienteId { get; private set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; private set; }

        public IEnumerable<ProdutoPedido> ProdutosPedido { get; private set; }  


        public Pedido()
        {
            Status = EPeditoStatus.Aberto;
        }

        public void SetCliente(Guid? clienteId)
        {
            ClienteId = clienteId;
        }

        public void SetCliente(Cliente cliente)
        {
            Cliente = cliente;
            ClienteId = cliente.Id;
        }

        public void AddProduto(ProdutoPedido produto)
        {
            ProdutosPedido ??= new List<ProdutoPedido>();
            ((List<ProdutoPedido>) ProdutosPedido).Add(produto);
        }

        public void SetStatus(EPeditoStatus cancelado)
        {
            Status = cancelado;
        }
    }
}
