using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Joao.Ana.Modas.App.Controllers
{
    public class MeuController : Controller
    {
        protected string GetUserId()
        {
            var cid = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals("ID"));
            return cid?.Value;
        }

        protected string GetUserPrimeiroNome()
        {
            var cid = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals("USER_PRIMEIRO_NOME"));
            return cid?.Value;
        }
        
        protected string GetUserNome()
        {
            var cid = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals("USER_NAME"));
            return cid?.Value;
        }

        protected string GetUserLogin()
        {
            var cid = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals("USER_LOGIN"));
            return cid?.Value;
        }

    }
}
