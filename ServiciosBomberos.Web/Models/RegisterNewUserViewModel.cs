namespace ServiciosBomberos.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterNewUserViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name ="Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Compare("Password")]
        [Display(Name ="Confirme Password")]
        public string Confirm { get; set; }
    }
}
