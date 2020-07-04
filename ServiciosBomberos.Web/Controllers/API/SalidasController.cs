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
    public class SalidasController : Controller
    {
        #region Atributos
        private readonly ISalidaRepository salidaRepository;
        private readonly IUserHelper userHelper;
        #endregion

        #region Constructor
        public SalidasController(
            ISalidaRepository salidaRepository,
            IUserHelper userHelper)
        {
            this.salidaRepository = salidaRepository;
            this.userHelper = userHelper;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public IActionResult GetSalida()
        {
            return Ok(this.salidaRepository.GetAllWithUsers());
        }

        [HttpPost]
        public async Task<IActionResult> PostSalida([FromBody] Common.Models.Salida salida)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(salida.User.Email);
            if (user == null)
            {
                return this.BadRequest("Usuario no Valido");
            }

            //TODO: Subir imagenes tomadas con el teléfono
            var entytySalida = new Salida
            {
                DiaSalida = salida.DiaSalida,
                Bombero1 = salida.Bombero1,
                EsReten1 = salida.EsReten1,
                Bombero2 = salida.Bombero2,
                EsReten2 = salida.EsReten2,
                HoraSalida = salida.HoraSalida,
                HoraRegreso = salida.HoraRegreso,
                TipoSalida = salida.TipoSalida,
                Descripcion = salida.Descripcion,
                User = user
            };

            var newSalida = await this.salidaRepository.CreateAsync(entytySalida);
            return Ok(newSalida);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalida([FromRoute] int id, [FromBody] Common.Models.Salida salida)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != salida.Id)
            {
                return BadRequest();
            }

            var oldSalida = await this.salidaRepository.GetByIdAsync(id);
            if (oldSalida == null)
            {
                return this.BadRequest("El Servicio no existe...");
            }

            //TODO: Subir imagenes tomadas con el teléfono
            oldSalida.DiaSalida = salida.DiaSalida;
            oldSalida.Bombero1 = salida.Bombero1;
            oldSalida.EsReten1 = salida.EsReten1;
            oldSalida.Bombero2 = salida.Bombero2;
            oldSalida.EsReten2 = salida.EsReten2;
            oldSalida.HoraSalida = salida.HoraSalida;
            oldSalida.HoraRegreso = salida.HoraRegreso;
            oldSalida.TipoSalida = salida.TipoSalida;
            oldSalida.Descripcion = salida.Descripcion;

            var updatedSalida = await this.salidaRepository.UpdateAsync(oldSalida);
            return Ok(updatedSalida);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalida([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var salida = await this.salidaRepository.GetByIdAsync(id);
            if (salida == null)
            {
                return this.NotFound();
            }
            await this.salidaRepository.DeleteAsync(salida);
            return Ok(salida);
        }
        #endregion
    }
}
