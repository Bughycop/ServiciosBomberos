namespace ServiciosBomberos.Web.Data
{
    using Entities;

    public class TipoRepository : GenericRepository<Tipo>, ITipoRepository
    {
        public TipoRepository(DataContext context) : base(context)
        {
        }
    }
}