namespace ServiciosBomberos.Web.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using ServiciosBomberos.Web.Data;

    [Route("api/[Controller]")]
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
            return Ok(this.tipoRepository.GetAll());
        }

        #endregion
    }
}
