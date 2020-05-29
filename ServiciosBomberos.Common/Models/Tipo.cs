namespace ServiciosBomberos.Common.Models
{
    using Newtonsoft.Json;


    public class Tipo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("prioridad")]
        public string Prioridad { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public override string ToString()
        {
            return $"{this.Nombre} {this.Prioridad}";
        }
    }
}
