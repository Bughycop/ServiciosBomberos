namespace ServiciosBomberos.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Helpers;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BomberosController : Controller
    {
        #region Atributos
        private readonly ISalidaRepository salidaRepository;
        #endregion

        #region Constructores
        public BomberosController(ISalidaRepository salidaRepository)
        {
            this.salidaRepository = salidaRepository;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public IActionResult GetBombero()
        {
            return Ok(this.salidaRepository.GetComboUsers());
        }
        #endregion
    }
}
