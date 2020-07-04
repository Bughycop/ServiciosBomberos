namespace ServiciosBomberos.Common.Models
{
    using Newtonsoft.Json;


    public class Tipo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("prioridad")]
        public string Prioridad { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

    }
}
