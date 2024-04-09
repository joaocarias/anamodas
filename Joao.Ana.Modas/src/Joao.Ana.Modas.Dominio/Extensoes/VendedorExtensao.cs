using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Dominio.Enums;

namespace Joao.Ana.Modas.Dominio.Extensoes
{
    public static class VendedorExtensao
    {
        public static decimal ObterValorComissao(this Vendedor vendedor, decimal valorTotal)
        {
            try
            {
                return vendedor.TipoComissao switch
                {
                    ETipoComissao.ValorFixo => vendedor.Comissao != null && vendedor.Comissao.GetValueOrDefault() > 0 ? vendedor.Comissao.Value : 0,
                    ETipoComissao.Porcentagem => vendedor.Comissao != null && vendedor.Comissao.GetValueOrDefault() > 0 ? (valorTotal * vendedor.Comissao.Value) / 100 : 0,
                    _ => 0,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
