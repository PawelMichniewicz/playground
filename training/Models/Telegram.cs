namespace Training.Models
{
    public class Telegram
    {
        public int ID { get; set; }

        public string Type { get; set; }

        public int Reading { get; set; }

        public ReadingQuality Quality { get; set; }
    }
}