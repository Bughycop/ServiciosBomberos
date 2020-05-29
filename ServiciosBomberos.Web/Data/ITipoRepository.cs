namespace ServiciosBomberos.Web.Data
{
    using System.Linq;
    using Entities;

    public interface ITipoRepository : IGenericRepository<Tipo>
    {
        IQueryable GetAllWithUsers();
    }
}
