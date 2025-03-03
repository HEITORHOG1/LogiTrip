using System.Text.Json.Serialization;

namespace LogiTrip.Model.Entity
{
    public class EnderecoDto
    {
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string Localidade { get; set; }

        [JsonPropertyName("uf")]
        public string UF { get; set; }
    }
}