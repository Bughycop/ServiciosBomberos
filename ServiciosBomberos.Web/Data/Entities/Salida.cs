namespace ServiciosBomberos.Web.Data.Entities
{
    using Org.BouncyCastle.Asn1.Cms;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Salida : IEntity
    {
        public int Id { get; set; }

        [Display(Name ="Dia de la Salida")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public DateTime DiaSalida { get; set; }

        [Display(Name ="Bombero")]
        [MaxLength(50)]
        [Required]
        public string Bombero1 { get; set; }

        [Display(Name = "Es Retén?")]
        public bool EsReten1 { get; set; }

        [Display(Name = "Bombero")]
        [MaxLength(50)]
        public string Bombero2 { get; set; }

        [Display(Name = "Es Retén?")]
        public bool EsReten2 { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [Display(Name ="Hora de inicio")]
        public DateTime HoraSalida { get; set; }

        [Display(Name ="Hora de regreso")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public DateTime HoraRegreso { get; set; }

        [Display(Name ="Tipo de Salida")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string TipoSalida { get; set; }

        [Display(Name ="Descripción")]
        public string Descripcion { get; set; }

        //[Display(Name ="Total horas")]
        //public TimeSpan TotalHorasSalida { get { return HoraRegreso - HoraSalida; } }

        public User User { get; set; }
    }
}
