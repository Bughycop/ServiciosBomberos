namespace ServiciosBomberos.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Salida
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("diaSalida")]
        public DateTime DiaSalida { get; set; }

        [JsonProperty("bombero1")]
        public string Bombero1 { get; set; }

        [JsonProperty("esReten1")]
        public bool EsReten1 { get; set; }

        [JsonProperty("bombero2")]
        public string Bombero2 { get; set; }

        [JsonProperty("esReten2")]
        public bool EsReten2 { get; set; }

        [JsonProperty("horaSalida")]
        public DateTime HoraSalida { get; set; }

        [JsonProperty("horaRegreso")]
        public DateTime HoraRegreso { get; set; }

        [JsonProperty("tipoSalida")]
        public string TipoSalida { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        //public TimeSpan TotalHorasSalida { get { return this.HoraRegreso - this.HoraSalida; } }

        [JsonProperty("user")]
        public User User { get; set; }

    }
}
