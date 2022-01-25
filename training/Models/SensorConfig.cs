namespace Training.Models
{
    class SensorConfig
    {
        public int ID { get; set; }

        public string Type { get; private set; }

        public int MinValue { get; private set; }

        public int MaxValue { get; private set; }

        public string Encoder { get; private set; }

        public int Frequency { get; private set; }
    }
}
