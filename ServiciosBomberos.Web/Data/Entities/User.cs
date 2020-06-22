
namespace ServiciosBomberos.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {
        [Required]
        [Display(Name = "Nombre")]
        [MaxLength(50,ErrorMessage ="El campo {0} solo puede tener {1} letras")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} solo puede tener {1} letras")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} solo puede tener {1} letras")]
        public string SegundoApellido { get; set; }

        [Display(Name ="Número de Teléfono")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get { return $"{this.Nombre} {this.PrimerApellido} {this.SegundoApellido}"; } }

        [Display(Name ="Bombero")]
        public string Malnom { get { return $"{this.Nombre} {this.PrimerApellido}"; }  }

        [NotMapped]
        [Display(Name ="Es Administrador?")]
        public bool IsAdmin { get; set; }
    }
}
