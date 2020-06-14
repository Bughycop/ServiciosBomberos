namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITipoRepository : IGenericRepository<Tipo>
    {
        IQueryable GetAllWithUsers();
        //Si no funciona lo quitamos
        IEnumerable<SelectListItem> GetComboTipos();
    }
}
