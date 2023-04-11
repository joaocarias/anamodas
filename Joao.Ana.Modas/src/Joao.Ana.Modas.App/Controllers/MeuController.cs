using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Joao.Ana.Modas.App.Controllers
{
    public class MeuController : Controller
    {
        protected string GetUserId()
        {
            var claim = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals(Constants.USER_ID));
            return claim?.Value;
        }

        protected string GetUserPrimeiroNome()
        {
            var claim = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals(Constants.USER_PRIMEIRO_NOME));
            return claim?.Value;
        }
        
        protected string GetUserNome()
        {
            var claim = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals(Constants.USER_NAME));
            return claim?.Value;
        }

        protected string GetUserLogin()
        {
            var claim = (HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(x => x.Type.Equals(Constants.USER_LOGIN));
            return claim?.Value;
        }

    }
}
