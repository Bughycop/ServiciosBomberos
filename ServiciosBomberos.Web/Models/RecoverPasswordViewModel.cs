namespace ServiciosBomberos.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
