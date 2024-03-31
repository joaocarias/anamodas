using System.Globalization;
using System.Text.RegularExpressions;

namespace Joao.Ana.Modas.Dominio.Extensoes
{
    public static class StringExtensao
    {
        public static string SomenteNumeros(this string? str)
        {
            if (str == null) return string.Empty;
            return Regex.Replace(str, "[^0-9]", "");
        }

        public static string PrimeiraParte(this string str)
        {
            if (str == null) return string.Empty;
            string[] parts = str.Split(' ');
            return parts[0];
        }

        public static string? DecimalToString(this decimal? numero)
        {
            return numero is null ? "0,00" :  numero?.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }
}
