namespace ServiciosBomberos.Web.Models
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string UsernName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordCompare { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
