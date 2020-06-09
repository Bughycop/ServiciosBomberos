namespace ServiciosBomberos.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Internal;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            //Add user
            var user = await this.userHelper.GetUserByEmailAsync("bughycop@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Nombre = "Jose",
                    PrimerApellido = "Robert",
                    SegundoApellido = "Janer",
                    Email = "bughycop@gmail.com",
                    UserName = "bughycop@gmail.com",
                    PhoneNumber = "630817017"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No se pudo crear el Usuario en Seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!this.context.Tipos.Any())
            {
                this.AddTipo("ABEJAS", "BAJA", user);
                this.AddTipo("ABRIR PUERTA", "MEDIA", user);
                this.AddTipo("INCENDIO DOMICILIO", "ALTA", user);
                await this.context.SaveChangesAsync();
            };
        }

        private void AddTipo(string nombre, string prioridad, User user)
        {
            this.context.Tipos.Add(new Tipo
            {
                Nombre = nombre,
                Prioridad = prioridad,
                User = user
            });
        }
    }
}