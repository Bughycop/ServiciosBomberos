namespace ServiciosBomberos.Web.Controllers.API
{
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TiposController : Controller
    {
        #region Atributos
        private readonly ITipoRepository tipoRepository;
        private readonly IUserHelper userHelper;
        #endregion

        #region Constructor
        public TiposController(
            ITipoRepository tipoRepository,
            IUserHelper userHelper)
        {
            this.tipoRepository = tipoRepository;
            this.userHelper = userHelper;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public IActionResult GetTipos()
        {
            return Ok(this.tipoRepository.GetAllWithUsers());
        }

        [HttpPost]
        public async Task<IActionResult> PostTipo([FromBody] Common.Models.Tipo tipo)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(tipo.User.Email);
            if (user == null)
            {
                return this.BadRequest("Usuario no valido");
            }

            var entityTipo = new Tipo
            {
                Nombre = tipo.Nombre,
                Prioridad = tipo.Prioridad,
                User = user
            };

            var newTipo = await this.tipoRepository.CreateAsync(entityTipo);
            return Ok(newTipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo([FromRoute] int id, [FromBody] Common.Models.Tipo tipo)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id!=tipo.Id)
            {
                return this.BadRequest();
            }

            var oldTipo = await this.tipoRepository.GetByIdAsync(id);
            if (oldTipo==null)
            {
                return this.BadRequest("El tipo no existe");
            }

            oldTipo.Nombre = tipo.Nombre;
            oldTipo.Prioridad = tipo.Prioridad;

            var updatedTipo = await this.tipoRepository.UpdateAsync(oldTipo);
            return Ok(updatedTipo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var tipo = await this.tipoRepository.GetByIdAsync(id);
            if (tipo==null)
            {
                return this.NotFound();
            }

            await this.tipoRepository.DeleteAsync(tipo);
            return Ok(tipo);
        }
        #endregion
    }
}
