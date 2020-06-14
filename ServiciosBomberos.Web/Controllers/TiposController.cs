namespace ServiciosBomberos.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TiposController : Controller
    {
        private readonly ITipoRepository tipoRepository;
        private readonly IUserHelper userHelper;

        public TiposController(ITipoRepository tipoRepository, IUserHelper userHelper)
        {
            this.tipoRepository = tipoRepository;
            this.userHelper = userHelper;
        }

        // GET: Tipos
        [Authorize]
        public IActionResult Index()
        {
            return View(this.tipoRepository.GetAll().OrderBy(t => t.Nombre));
        }

        // GET: Tipos/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("TipoNotFound");
            }

            var tipo = await this.tipoRepository.GetByIdAsync(id.Value);

            if (tipo == null)
            {
                return new NotFoundViewResult("TipoNotFound");
            }

            return View(tipo);
        }

        // GET: Tipos/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                tipo.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await this.tipoRepository.CreateAsync(tipo);
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        // GET: Tipos/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("TipoNotFound");
            }

            var tipo = await this.tipoRepository.GetByIdAsync(id.Value);
            if (tipo == null)
            {
                return new NotFoundViewResult("TipoNotFound");
            }
            return View(tipo);
        }

        // POST: Tipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tipo.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await this.tipoRepository.UpdateAsync(tipo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.tipoRepository.ExistAsync(tipo.Id))
                    {
                        return new NotFoundViewResult("TipoNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        // GET: Tipos/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("TipoNotFound");
            }

            var tipo = await this.tipoRepository.GetByIdAsync(id.Value);
            if (tipo == null)
            {
                return new NotFoundViewResult("TipoNotFound");
            }

            return View(tipo);
        }

        // POST: Tipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipo = await this.tipoRepository.GetByIdAsync(id);
            await this.tipoRepository.DeleteAsync(tipo);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TipoNotFound()
        {
            return this.View();
        }
    }
}
