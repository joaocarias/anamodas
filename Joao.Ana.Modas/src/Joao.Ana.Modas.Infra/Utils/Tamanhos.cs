namespace Joao.Ana.Modas.Infra.Utils
{
    public static class Tamanhos
    {
        public static Dictionary<string, string> GetLista() => 
            new Dictionary<string, string>
            {
                { "PP", "PP" },
                { "P", "P" }, 
                { "M", "M" },
                { "G", "G" },
                { "GG", "GG" }
            };
    }
}
