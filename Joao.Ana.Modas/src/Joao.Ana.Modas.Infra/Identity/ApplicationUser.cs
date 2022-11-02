using Microsoft.AspNetCore.Identity;

namespace Joao.Ana.Modas.Infra.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set;}
    }
}
