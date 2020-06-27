namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class SalidaRepository : GenericRepository<Salida>, ISalidaRepository
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public SalidaRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Salidas.Include(s => s.User).OrderByDescending(s => s.Id);
        }

        public IEnumerable<SelectListItem> GetComboUsers()
        {
            var list = this.context.Users.Select(u => new SelectListItem
            {
                Text = u.Malnom,
                Value = u.Malnom
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un Bombero...",
                Value = string.Empty
            });

            return list;
        }
    }
}
