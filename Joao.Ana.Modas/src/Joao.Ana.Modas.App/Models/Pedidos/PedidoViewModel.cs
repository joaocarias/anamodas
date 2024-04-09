using Joao.Ana.Modas.App.Models.Clientes;
using Joao.Ana.Modas.App.Models.TipoPagamento;
using Joao.Ana.Modas.App.Models.Vendedores;
using Joao.Ana.Modas.Dominio.Enums;
using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.App.Models.Pedidos
{
    public class PedidoViewModel : EntidadeBaseViewModel
    {
        public Guid? ClienteId { get; set; }

        public ClienteViewModel? Cliente { get; set; }

        public bool ClienteAnonimo { get; set; } = false;

        public EPeditoStatus Status { get; set; }
               
        [Display(Name = "Forma de Pagamento")]
        public Guid? TipoPagamentoId { get; set; }  

        public TipoPagamentoViewModel? TipoPagamento { get; set; }

        public VendedorViewModel? Vendedor { get; set; }

        public Guid? VendedorId { get; set; }

        public decimal Comissao { get; set; } = decimal.Zero;

        public decimal Total { get; set; } = decimal.Zero;  
    }
}
