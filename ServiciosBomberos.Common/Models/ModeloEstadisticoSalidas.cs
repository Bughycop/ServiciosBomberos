namespace ServiciosBomberos.Common.Models
{
    using System;

    public class ModeloEstadisticoSalidas
    {
        public string TipoServicio { get; set; }

        public int NumeroServicios { get; set; }
        
        public TimeSpan NumeroHorasTipo { get; set; }

        //public TimeSpan NumeroTotalhoras { get; set; }
    }
}
