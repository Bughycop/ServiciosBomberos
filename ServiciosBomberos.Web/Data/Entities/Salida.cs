namespace ServiciosBomberos.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net.NetworkInformation;

    public class Salida
    {
        public int Id { get; set; }

        //[Display(Name ="Es Retén")]
        //public bool EsReten1 { get; set; }

        //[Display(Name ="Es Retén")]
        //public bool EsReten2 { get; set; }

        [Display(Name ="Tipo de Salida")]
        public Tipo TipoSalida { get; set; }

        [Display(Name ="Descripción")]
        public string Descripcion { get; set; }
    }
}
