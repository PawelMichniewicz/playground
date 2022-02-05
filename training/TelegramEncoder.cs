using Training.Models;

namespace Training
{
    internal class TelegramEncoder
    {
        private readonly SensorConfig config;

        public TelegramEncoder(SensorConfig sensorConfig)
        {
            config = sensorConfig;
        }

        public string Encode(int reading, QualityClassifier.ReadingQuality quality)
        {
            return $"$FIX,{config.ID},{config.Type},{reading},{quality}*";
        }
    }
}