using Joao.Ana.Modas.Dominio.Enums;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Pedido : EntidadeBase
    {
        public EPeditoStatus Status { get; private set; }

        public Pedido()
        {
            Status = EPeditoStatus.Aberto;
        }
    }
}
