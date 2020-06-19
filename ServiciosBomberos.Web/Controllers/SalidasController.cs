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
    using Microsoft.EntityFrameworkCore;
    using Models;

    [Authorize]
    public class SalidasController : Controller
    {
        #region Atributos
        private readonly ISalidaRepository salidaRepository;
        private readonly ITipoRepository tipoRepository;
        private readonly IUserHelper userHelper;
        #endregion

        #region Constructores
        public SalidasController(
            ISalidaRepository salidaRepository,
            ITipoRepository tipoRepository,
            IUserHelper userHelper)

        {
            this.salidaRepository = salidaRepository;
            this.tipoRepository = tipoRepository;
            this.userHelper = userHelper;
        }
        #endregion

        #region Acciones
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
            view.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            if (ModelState.IsValid)
            {
                var salida = this.ToSalida(view);
                await this.salidaRepository.CreateAsync(salida);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Create));
            }

        }
        // GET: Tipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("SalidaNotFound");
            }

            var model = await this.salidaRepository.GetByIdAsync(id.Value);
            if (model == null)
            {
                return new NotFoundViewResult("SalidaNotFound");
            }
            var view = this.ToViewModel(model);
            view.TiposDeSalida = this.tipoRepository.GetComboTipos();
            view.Bomberos = this.salidaRepository.GetComboUsers();
            view.Bomberos2 = this.salidaRepository.GetComboUsers();

            return View(view);
        }

        // POST: Salidas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddSalidaViewModel view)
        {

            if (ModelState.IsValid)
            {
                var salida = this.ToSalida(view);

                try
                {
                    await this.salidaRepository.UpdateAsync(salida);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.salidaRepository.ExistAsync(salida.Id))
                    {
                        return new NotFoundViewResult("SalidaNotFound");
                    }

                    else
                    {
                        throw;

                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        //POST:Salidas/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("SalidaNotFound");
            }

            var salida = await this.salidaRepository.GetByIdAsync(id.Value);
            if (salida == null)
            {
                return new NotFoundViewResult("SalidaNotFound");
            }

            return View(salida);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salida = await this.salidaRepository.GetByIdAsync(id);
            await this.salidaRepository.DeleteAsync(salida);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SalidaNotFound()
        {
            return this.View();
        }
        #endregion

        #region Metodos
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
                HoraSalida=view.HoraSalida,
                HoraRegreso=view.HoraRegreso,
                User = view.User
            };
        }

        private AddSalidaViewModel ToViewModel(Salida model)
        {
            return new AddSalidaViewModel
            {
                Id = model.Id,
                DiaSalida = model.DiaSalida,
                Bombero1 = model.Bombero1,
                EsReten1 = model.EsReten1,
                Bombero2 = model.Bombero2,
                EsReten2 = model.EsReten2,
                TipoSalida = model.TipoSalida,
                Descripcion = model.Descripcion,
                HoraRegreso=model.HoraRegreso,
                HoraSalida=model.HoraSalida,
                User = model.User
            };
        }

        #endregion
    }
}

