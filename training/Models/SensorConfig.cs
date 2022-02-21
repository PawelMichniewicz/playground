namespace Training.Models
{
    public class SensorConfig
    {
        public int ID { get; set; }

        public string Type { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public string Encoder { get; set; }

        public int Frequency { get; set; }
    }
}
