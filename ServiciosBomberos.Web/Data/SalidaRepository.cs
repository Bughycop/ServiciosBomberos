namespace ServiciosBomberos.Web.Data
{
    using ServiciosBomberos.Web.Data.Entities;

    public class SalidaRepository : GenericRepository<Salida>, ISalidaRepository
    {
        public SalidaRepository(DataContext context) : base(context)
        {
        }
    }
}
