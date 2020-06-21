namespace ServiciosBomberos.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [EmailAddress]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
