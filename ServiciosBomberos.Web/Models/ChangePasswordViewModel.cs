namespace ServiciosBomberos.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Password actual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name ="Nuevo Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Compare("NewPassword")]
        [Display(Name ="Confirme Password")]
        public string Confirm { get; set; }
    }
}
