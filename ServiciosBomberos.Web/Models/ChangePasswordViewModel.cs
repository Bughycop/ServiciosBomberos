namespace ServiciosBomberos.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name ="Password actual")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name ="Nuevo Password")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        [Display(Name ="Confirme Password")]
        public string Confirm { get; set; }
    }
}
