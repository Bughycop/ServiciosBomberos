
namespace ServiciosBomberos.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string NombreCompleto { get { return $"{this.Nombre} {this.PrimerApellido} {this.SegundoApellido}"; } }

        public string Malnom { get { return $"{this.Nombre} {this.PrimerApellido}"; }  }
    }
}
