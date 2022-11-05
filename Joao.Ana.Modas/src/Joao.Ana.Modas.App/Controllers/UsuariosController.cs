using Joao.Ana.Modas.App.Models.Usuarios;
using Joao.Ana.Modas.Infra.Identity;
using Joao.Ana.Modas.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Joao.Ana.Modas.App.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRADOR)]
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsuariosController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registrar() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,  
                    Nome = model.Nome,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User) && User.IsInRole(Constants.ADMINISTRADOR))
                    {
                        return RedirectToAction("Index", "Usuarios");
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Login Inválido");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AcessoNegado()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {id} não foi encontrado";
                return View("NotFound");
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditarViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Nome = user.Nome,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {model.Id} não foi encontrado";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Nome = model.Nome;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {id} não foi encontrado";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }
        }
    }
}
