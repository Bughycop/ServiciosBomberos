namespace ServiciosBomberos.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tipo
    {
        public int Id { get; set; }

        [Display(Name ="Tipo de Salida")]
        public string Nombre { get; set; }

        [Display(Name ="Prioridad")]
        public string Prioridad { get; set; }
    }
}
