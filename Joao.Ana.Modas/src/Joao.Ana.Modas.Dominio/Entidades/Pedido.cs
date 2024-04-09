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

        public Guid? TipoPagamentoId { get; private set; }  

        public TipoPagamento? TipoPagamento { get; private set; }

        public Vendedor? Vendedor { get; private set; }

        public Guid? VendedorId { get; private set; }   

        public decimal Comissao { get; private set; } = decimal.Zero;

        public decimal Total { get; private set; } = decimal.Zero;

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

        public void SetStatus(EPeditoStatus status)
        {
            Status = status;
            Atualizar();
        }

        public void Cancelar()
        {
            Status = EPeditoStatus.Cancelado;
            Atualizar();
        }

        public void Finalizar(Guid? vendedorId = null, decimal total = 0, decimal comissao = 0)
        {
            Status = EPeditoStatus.Finalizado;
            Total = total;
            Comissao = comissao;
            VendedorId = vendedorId;    
            Atualizar();
        }

        public void SetTipoPagamento(Guid? tipoPagamentoId)
        {
            TipoPagamentoId = tipoPagamentoId;
        }

        public void SetVendedor(Guid? vendedorId)
        {
            VendedorId = vendedorId;
        }

        public void SetComissao(decimal comissao)
        {
            Comissao = comissao;    
        }
    }
}
