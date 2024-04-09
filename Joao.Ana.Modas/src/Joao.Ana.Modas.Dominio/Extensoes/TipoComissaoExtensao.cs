using Joao.Ana.Modas.Dominio.Enums;

namespace Joao.Ana.Modas.Dominio.Extensoes
{
    public static class TipoComissaoExtensao
    {        
        public static string ToStringParse(this ETipoComissao? tipoComissiao)
        {            
            if (tipoComissiao == null) return string.Empty;
            return tipoComissiao.ToStringParse();   
        }

        public static string ToStringParse(this ETipoComissao tipoComissao)
        {
            return tipoComissao switch
            {
                ETipoComissao.ValorFixo => "Valor Fixo",
                ETipoComissao.Porcentagem => "Porcentagem",
                ETipoComissao.Desconhecido => "Desconhecido",
                _ => string.Empty
            };
        }
    }
}
