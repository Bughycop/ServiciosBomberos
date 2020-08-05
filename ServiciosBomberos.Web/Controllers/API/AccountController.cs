namespace ServiciosBomberos.Web.Controllers.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ServiciosBomberos.Common.Models;
    using ServiciosBomberos.Web.Helpers;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class AccountController : Controller
    {
        #region Atributos
        private readonly IUserHelper userHelper;
        private readonly IMailHelper mailHelper;
        #endregion

        #region Constructores
        public AccountController(IUserHelper userHelper, IMailHelper mailHelper)
        {
            this.userHelper = userHelper;
            this.mailHelper = mailHelper;
        }
        #endregion

        #region Metodos
        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await this.userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Este correo no esta asignado a ningún usuario"
                });
            }

            var myToken = this.userHelper.GeneratePasswordResetTokenAsync(user);
            var link = this.Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
            this.mailHelper.SendMail(request.Email, "Recuperar Contraseña", $"<h1>Cambie la Contraseña</h1>" +
                $"Para cambiar la contraseña haga click en el siguiente link: </br></br>" +
                $"<a href = \"{link}\">Cambie la contraseña</a>");

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "Un Email con instrucciones para cambiar la contraseña ha sido enviado a su Correo"
            });
        }

        [HttpPost]
        [Route("GetUserByEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserByEmail([FromBody] RecoverPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await this.userHelper.GetUserByEmailAsync(request.Email);
            if (user==null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "El Usuario no existe"
                });
            }

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userEntity = await this.userHelper.GetUserByEmailAsync(user.Email);
            if (userEntity==null)
            {
                return this.BadRequest("El Usuario no existe");
            }

            userEntity.Nombre = user.Nombre;
            userEntity.PrimerApellido = user.PrimerApellido;
            userEntity.SegundoApellido = user.SegundoApellido;
            userEntity.PhoneNumber = user.PhoneNumber;

            var response = await this.userHelper.UpdateUserAsync(userEntity);
            if (!response.Succeeded)
            {
                return this.BadRequest(response.Errors.FirstOrDefault().Description);
            }

            var updatedUser = await this.userHelper.GetUserByEmailAsync(user.Email);
            return Ok(updatedUser);
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await this.userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "El Email no esta asignado a ningún Usuario"
                });
            }

            var result = await this.userHelper.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault().Description
                });
            }

            return this.Ok(new Response
            {
                IsSuccess = true,
                Message = "La Contraseña ha sido cambiada con éxito"
            });
        }
        #endregion
    }
}
