using Joao.Ana.Modas.Dominio.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Pedido : EntidadeBase
    {
        public EPeditoStatus Status { get; private set; } = EPeditoStatus.Aberto;

        public Guid? ClienteId { get; private set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; private set; }

        public bool ClienteAnonimo { get; private set; } = false;
               
        public Pedido()
        {
           
        }
        
        public void SetCliente(Guid? clienteId = null)
        {
            ClienteId = clienteId;
            ClienteAnonimo = clienteId is null;
            if (clienteId is null) Cliente = null;
        }

        public void SetCliente(Cliente cliente)
        {
            Cliente = cliente;
            ClienteId = cliente.Id;
            ClienteAnonimo = false;
        }

        public void SetStatus(EPeditoStatus cancelado)
        {
            Status = cancelado;
        }
    }
}
