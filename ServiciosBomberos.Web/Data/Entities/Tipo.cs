namespace ServiciosBomberos.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tipo
    {
       public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El campo {0} sólo puede contener {1} caracteres")]
        [Display(Name = "Tipo de Salida")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="El campo {0} sólo puede contener {1} caracteres")]
        [Display(Name = "Prioridad")]
        public string Prioridad { get; set; }
    }
}
