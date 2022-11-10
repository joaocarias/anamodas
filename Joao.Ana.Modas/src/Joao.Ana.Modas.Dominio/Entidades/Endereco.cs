using System.ComponentModel.DataAnnotations;

namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class Endereco : EntidadeBase
    {
        [StringLength(100)]
        public string? Logradouro { get; private set; }

        [StringLength(10)]
        public string? Numero { get; private set; }

        [StringLength(100)]
        public string? Bairro { get; private set; }

        [StringLength(100)]
        public string? Complemento { get; private set; }

        [StringLength(20)]
        public string? Cep { get; private set; }

        [StringLength(100)]
        public string? Cidade { get; private set; }

        [StringLength(2)]
        public string? Estado { get; private set; }

        public Endereco(string? logradouro, string? numero, string? bairro, string? complemento, string? cep, string? cidade, string? estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }

        private Endereco() { }
    }
}
