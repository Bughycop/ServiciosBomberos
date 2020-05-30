namespace ServiciosBomberos.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class AccountController : Controller
    {
        #region Atributos
        private readonly IUserHelper userHelper;
        #endregion

        #region Constructores
        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }
        #endregion

        #region Metodos
        //GET: Account/Login
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        //POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Fallo al Iniciar Sesión");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        //GET: Account/Register
        public IActionResult Register()
        {
            return this.View();
        }

        //POST: Account/Register
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.UserName);
                if (user == null)
                {
                    user = new User
                    {
                        Nombre = model.Nombre,
                        PrimerApellido = model.PrimerApellido,
                        SegundoApellido = model.SegundoApellido,
                        Email = model.UserName,
                        UserName = model.UserName
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "El Usuario no pudo ser creado");
                        return this.View(model);
                    }

                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.UserName
                    };
                    
                    var result2 = await this.userHelper.LoginAsync(loginViewModel);
                    
                    if (result2.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, "El Ususario no pudo iniciar Sesión");
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "El Usuario ya está registrado");

            }
            return this.View(model);
        }


        #endregion
    }
}
