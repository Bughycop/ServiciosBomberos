namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class TipoRepository : GenericRepository<Tipo>, ITipoRepository
    {
        private readonly DataContext context;

        public TipoRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Tipos.Include(t => t.User).OrderBy(t => t.Nombre);
        }

        public IEnumerable<SelectListItem> GetComboTipos()
        {
            var list = this.context.Tipos.Select(t => new SelectListItem
            {
                Text = t.Nombre,
                Value = t.Nombre
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un Tipo de Salida...",
                Value = string.Empty
            });

            return list;
        }
    }
}