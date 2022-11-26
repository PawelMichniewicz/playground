using System.Text.Json.Serialization;

namespace Training.Models
{
    public class SensorConfig
    {
        public int ID { get; set; }

        public string Type { get; set; } = string.Empty;

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        [JsonPropertyName("EncoderType")]
        public string Encoder { get; set; } = string.Empty;

        public int Frequency { get; set; }
    }
}
