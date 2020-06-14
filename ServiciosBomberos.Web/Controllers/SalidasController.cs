namespace ServiciosBomberos.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class SalidasController : Controller
    {
        private readonly ISalidaRepository salidaRepository;
        private readonly ITipoRepository tipoRepository;
        private readonly IUserHelper userHelper;

        public SalidasController(
            ISalidaRepository salidaRepository,
            ITipoRepository tipoRepository,
            IUserHelper userHelper)

        {
            this.salidaRepository = salidaRepository;
            this.tipoRepository = tipoRepository;
            this.userHelper = userHelper;
        }

        //GET: Salidas
        [Authorize]
        public IActionResult Index()
        {
            return this.View(this.salidaRepository.GetAll().OrderByDescending(s => s.Id));
        }

        //GET: Salidas/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("SalidaNotFound");
            }

            var salida = await this.salidaRepository.GetByIdAsync(id.Value);
            return View(salida);
        }

        //GET: Salidas/Create
        public IActionResult Create()
        {
            var model = new AddSalidaViewModel
            {
                DiaSalida = DateTime.UtcNow.Date,
                TiposDeSalida = this.tipoRepository.GetComboTipos(),
                Bomberos = this.salidaRepository.GetComboUsers(),
                Bomberos2 = this.salidaRepository.GetComboUsers()
            };

            return this.View(model);
        }

        // POST: Salidas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddSalidaViewModel view)
        {

            if (ModelState.IsValid)
            {
                var salida = this.ToSalida(view);
                salida.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await this.salidaRepository.CreateAsync(salida);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Create));
            }

        }

        private Salida ToSalida(AddSalidaViewModel view)
        {
            return new Salida
            {
                Id = view.Id,
                Bombero1 = view.Bombero1,
                EsReten1 = view.EsReten1,
                Bombero2 = view.Bombero2,
                EsReten2 = view.EsReten2,
                DiaSalida = view.DiaSalida,
                TipoSalida = view.TipoSalida,
                Descripcion = view.Descripcion,
                User = view.User
            };
        }
    }
}
