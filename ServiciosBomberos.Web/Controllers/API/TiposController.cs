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
        #region Atributos
        private readonly ITipoRepository tipoRepository; 
        #endregion

        #region Constructor
        public TiposController(ITipoRepository tipoRepository)
        {
            this.tipoRepository = tipoRepository;
        } 
        #endregion

        #region Metodos
        [HttpGet]
        public IActionResult GetTipos()
        {
            return Ok(this.tipoRepository.GetAllWithUsers());
        }

        #endregion
    }
}
