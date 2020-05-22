namespace ServiciosBomberos.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using ServiciosBomberos.Web.Data.Entities;

    public class DataContext : DbContext
    {
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
