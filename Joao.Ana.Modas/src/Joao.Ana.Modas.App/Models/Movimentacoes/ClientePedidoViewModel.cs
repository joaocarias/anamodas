using Joao.Ana.Modas.App.Models.Caixa;
using Joao.Ana.Modas.App.Models.Clientes;

namespace Joao.Ana.Modas.App.Models.Movimentacoes
{
    public class ClientePedidoViewModel
    {
        public Guid? ClienteId { get; set; }

        public ClienteViewModel? Cliente { get; set; }
                
        public bool Anonimo { get; set; }

        public ClientePedidoViewModel()
        {

        }
    }
}
