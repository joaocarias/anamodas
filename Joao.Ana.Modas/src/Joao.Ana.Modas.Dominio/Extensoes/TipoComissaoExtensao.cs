using Joao.Ana.Modas.Dominio.Enums;

namespace Joao.Ana.Modas.Dominio.Extensoes
{
    public static class TipoComissaoExtensao
    {        
        public static string ToStringParse(this ETipoComissiao? tipoComissiao)
        {            
            return tipoComissiao switch
            {
                ETipoComissiao.ValorFixo => "Valor Fixo",
                ETipoComissiao.Porcentagem => "Porcentagem",
                ETipoComissiao.Desconhecido => "Desconhecido",
                _ => string.Empty
            };         
        }
    }
}
