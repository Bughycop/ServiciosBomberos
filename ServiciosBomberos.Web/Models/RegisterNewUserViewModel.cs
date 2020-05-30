namespace ServiciosBomberos.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterNewUserViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name ="Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name ="Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name ="Confirme Password")]
        public string Confirm { get; set; }
    }
}
