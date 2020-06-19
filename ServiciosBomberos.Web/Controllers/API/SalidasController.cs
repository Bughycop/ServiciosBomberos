namespace ServiciosBomberos.Web.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using ServiciosBomberos.Web.Data;

    [Route("api/[Controller]")]
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
            return Ok(this.salidaRepository.GetAll());
        }
        #endregion
    }
}
