﻿namespace ServiciosBomberos.Web.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Models;

    public class AccountController : Controller
    {
        #region Atributos
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;
        private readonly IMailHelper mailHelper;
        #endregion

        #region Constructores
        public AccountController(
            IUserHelper userHelper,
            IConfiguration configuration,
            IMailHelper mailHelper)
        {
            this.userHelper = userHelper;
            this.configuration = configuration;
            this.mailHelper = mailHelper;
        }
        #endregion

        #region Acciones
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
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return this.View();
        }

        //POST: Account/Register
        [HttpPost]
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
                        PhoneNumber=model.NumeroTelefono,
                        Email = model.UserName,
                        UserName = model.UserName
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "El Usuario no pudo ser creado");
                        return this.View(model);
                    }

                    var myToken = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = this.Url.Action("ConfirmEmail",
                        "Account",
                        new
                            {
                                userid = user.Id,
                                token = myToken
                            },
                        protocol: HttpContext.Request.Scheme);

                    this.mailHelper.SendMail(model.UserName, "Confirmación de correo de Gestion de Bomberos Maó", $"<h1>Bomberos Maó Confirmación de correo</h1>" +
                        $"Para habilitar el Usuario" + 
                        $"por favor haca click en el link:<br></br><a href= \"{tokenLink}\">Confirme su Email</a><br></br>" +
                        $"Su nombre de Usuario será su Email completo y su contraseña su nombre de Usuario sin incluir @xxx.xxx seguido de su numero de teléfono");
                    this.ViewBag.Message = "Las instrucciones para validar su Usuario han sido enviadas a su Email";
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "El Usuario ya está registrado");

            }
            return this.View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await this.userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }
            return View();
        }

        // GET: Account/ChangeUser
        [Authorize]
        public async Task<IActionResult> ChangeUser()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();
            if (user != null)
            {
                model.Nombre = user.Nombre;
                model.PrimerApellido = user.PrimerApellido;
                model.SegundoApellido = user.SegundoApellido;
                model.NumeroTelefono = user.PhoneNumber;
            }

            return this.View(model);
        }

        //POST: Account/ChangeUser
        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.Nombre = model.Nombre;
                    user.PrimerApellido = model.PrimerApellido;
                    user.SegundoApellido = model.SegundoApellido;
                    user.PhoneNumber = model.NumeroTelefono;
                    var response = await this.userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        this.ViewBag.UserMessage = "Usuario actualizado!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Usuario no encontrado");
                }
            }

            return this.View(model);
        }

        //GET: Account/ChangePassword
        [Authorize]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        //POST: Account/ChangePassword
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Usuario no encontrado");
                }
            }
            return this.View(model);
        }

        //POST: no tiene vista
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await this.userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            this.configuration["Tokens:Issuer"],
                            this.configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

        public IActionResult NotAutorized()
        {
            return this.View();
        }

        public IActionResult UserNotAuthorized()
        {
            return this.View();
        }

        //Account/RecoverPassword
        public IActionResult RecoverPassword()
        {
            return this.View();
        }

        //Post:Account/RecoverPassword
        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "El Correo no corresponde a un usuario registrado");
                    return this.View(model);
                }

                var myToken = await this.userHelper.GeneratePasswordResetTokenAsync(user);
                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken },
                    protocol: HttpContext.Request.Scheme);
                var mailSender = new MailHelper(configuration);
                mailSender.SendMail(model.Email, "Recuperar password Servicios Bomberos", $"<h1>Recupere la contraseña<h1/>" +
                    $"Para recuperar el password haga click en el siguiente enlace:</br></br>" +
                    $"<a href=\"{link}\">Reset password</a>");
                this.ViewBag.Message = "Las instrucciones para recuperar la contraseña han sido enviadas a su correo electrónico";
                return this.View();
            }
            return this.View(model);
        }

        //Get:Account/ResetPassword
        public IActionResult ResetPassword(string token)
        {
            return this.View();
        }

        //Post:Account/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await this.userHelper.GetUserByEmailAsync(model.UsernName);
            if (user != null)
            {
                var result = await this.userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "La contraseña ha sido recuperada con éxtito";
                    return this.View();
                }
                this.ViewBag.Message = "Ha ocurrido un error al recuperar la contraseña";
                return this.View(model);
            }
            this.ViewBag.Message = "Usuario no encontrado...";
            return this.View(model);
        }

        #endregion
        #region Administrador Usuarios
        //Get: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await this.userHelper.GetAllUsersAsync();
            foreach (var user in users)
            {
                var myUser = await this.userHelper.GetUserByIdAsync(user.Id);
                if (myUser != null)
                {
                    user.IsAdmin = await this.userHelper.IsUserInRoleAsync(myUser, "Admin");
                }
            }
            return this.View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOff(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.RemoveUserFromRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.AddUserToRoleAsync(user, "Admin");
            return this.RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.DeleteUserAsync(user);
            return this.RedirectToAction(nameof(Index));
        } 
        #endregion
    }
}
