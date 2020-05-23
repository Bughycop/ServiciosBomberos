namespace ServiciosBomberos.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Internal;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Tipos.Any())
            {
                this.AddTipo("ABEJAS", "BAJA");
                this.AddTipo("ABRIR PUERTA", "MEDIA");
                this.AddTipo("INCENDIO DOMICILIO", "ALTA");
                await this.context.SaveChangesAsync();
            };
        }

        private void AddTipo(string nombre, string prioridad)
        {
            this.context.Tipos.Add(new Tipo 
            {
                Nombre = nombre,
                Prioridad = prioridad
            });
        }
    }
}