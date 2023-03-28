using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class CaixaController : MeuController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
