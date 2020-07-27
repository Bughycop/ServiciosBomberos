namespace ServiciosBomberos.Common.Models
{
    using Newtonsoft.Json;

    public class Bombero
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
