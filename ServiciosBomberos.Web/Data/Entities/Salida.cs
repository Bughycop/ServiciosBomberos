namespace ServiciosBomberos.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net.NetworkInformation;

    public class Salida : IEntity
    {
        public int Id { get; set; }

        [Display(Name ="Dia de la Salida")]

        public DateTime DiaSalida { get; set; }

        [Display(Name ="Bombero")]
        [MaxLength(50)]
        public string Bombero1 { get; set; }

        [Display(Name = "Es Retén?")]
        public bool EsReten1 { get; set; }

        [Display(Name = "Bombero")]
        [MaxLength(50)]
        public string Bombero2 { get; set; }

        [Display(Name = "Es Retén?")]
        public bool EsReten2 { get; set; }

        [Display(Name ="Tipo de Salida")]
        [Required]
        public Tipo TipoSalida { get; set; }

        [Display(Name ="Descripción")]
        public string Descripcion { get; set; }

        public User User { get; set; }
    }
}
