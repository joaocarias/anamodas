using Joao.Ana.Modas.App.Models.Regras;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    public class RegrasController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RegrasController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarViewModel model)
        {
            if (ModelState.IsValid)
            {
                // precisamos apenas especificar o nome único da role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Salva a role na tabela AspNetRole
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Regras");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
