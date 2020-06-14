namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ServiciosBomberos.Web.Models;

    public interface ISalidaRepository : IGenericRepository<Salida>
    {
        //Task AdItemToSalidaAsync(Salida model, string username);
        IEnumerable<SelectListItem> GetComboUsers();

    }
}
