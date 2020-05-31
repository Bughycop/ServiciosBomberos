namespace ServiciosBomberos.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TiposController : Controller
    {
        private readonly ITipoRepository tipoRepository;

        public TiposController(ITipoRepository tipoRepository)
        {
            this.tipoRepository = tipoRepository;
        }

        #region Metodos
        [HttpGet]
        public IActionResult GetTipos()
        {
            return Ok(this.tipoRepository.GetAllWithUsers());
        }

        #endregion
    }
}
