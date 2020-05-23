namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Tipo> GetTipos()
        {
            return this.context.Tipos.OrderBy(p => p.Nombre);
        }

        public Tipo GetTipo(int id)
        {
            return this.context.Tipos.Find(id);
        }

        public void AddTipo(Tipo Tipo)
        {
            this.context.Tipos.Add(Tipo);
        }

        public void UpdateTipo(Tipo Tipo)
        {
            this.context.Tipos.Update(Tipo);
        }

        public void RemoveTipo(Tipo Tipo)
        {
            this.context.Tipos.Remove(Tipo);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool TipoExists(int id)
        {
            return this.context.Tipos.Any(p => p.Id == id);
        }
    }
}
