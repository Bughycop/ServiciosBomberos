namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ServiciosBomberos.Web.Data.Entities;
    using ServiciosBomberos.Web.Helpers;

    public class SalidaRepository : GenericRepository<Salida>, ISalidaRepository
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public SalidaRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        //NUEVO (no se si es necesario)
        //public async Task AdItemToSalidaAsync(Salida model, string username)
        //{
        //    var user = await this.userHelper.GetUserByEmailAsync(username);

        //    if (user == null)
        //    {
        //        return;
        //    }

        //    var salida = await this.context.Salidas.FindAsync(model.Id);
        //    if (salida == null)
        //    {
        //        salida = new Salida
        //        {
        //            DiaSalida = model.DiaSalida,
        //            TipoSalida = model.TipoSalida,
        //            Bombero1 = model.Bombero1,
        //            EsReten1 = model.EsReten1,
        //            Bombero2 = model.Bombero2,
        //            EsReten2 = model.EsReten2,
        //            Descripcion = model.Descripcion,
        //            User = user
        //        };

        //        this.context.Salidas.Add(salida);
        //    }
        //    else
        //    {
        //        return;
        //    }

        //    await this.context.SaveChangesAsync();
        //}

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
