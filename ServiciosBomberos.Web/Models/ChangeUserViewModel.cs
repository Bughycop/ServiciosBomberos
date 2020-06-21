namespace ServiciosBomberos.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ChangeUserViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

    }
}
