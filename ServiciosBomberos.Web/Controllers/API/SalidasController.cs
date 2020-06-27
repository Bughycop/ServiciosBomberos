namespace ServiciosBomberos.Web.Controllers.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ServiciosBomberos.Web.Data;
    using System.Net;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalidasController : Controller
    {
        #region Atributos
        private readonly ISalidaRepository salidaRepository;
        #endregion

        #region Constructor
        public SalidasController(ISalidaRepository salidaRepository)
        {
            this.salidaRepository = salidaRepository;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public IActionResult GetSalida()
        {
            return Ok(this.salidaRepository.GetAllWithUsers());
        }
        #endregion
    }
}
