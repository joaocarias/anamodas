using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joao.Ana.Modas.Infra.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set;}

        [NotMapped]
        public string PrimeiroNome
        {
            get
            {
                string[] nomePartes = Nome.Split(' ');
                return nomePartes[0];
            }
        }
    }
}
