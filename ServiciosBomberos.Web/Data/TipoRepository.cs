namespace ServiciosBomberos.Web.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class TipoRepository : GenericRepository<Tipo>, ITipoRepository
    {
        private readonly DataContext context;

        public TipoRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Tipos.Include(t=> t.User).OrderBy(t=>t.Nombre);
        }
    }
}