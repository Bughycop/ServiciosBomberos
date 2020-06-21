namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ISalidaRepository : IGenericRepository<Salida>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboUsers();

    }
}
