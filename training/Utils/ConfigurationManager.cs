using Training.Interfaces;
using Training.Models;
using Training.Parsers;

namespace Training.Utils
{
    public class ConfigurationManager
    {
        private const string configPath = @".\..\..\..\Config\";
        private const string sensorConfigFileName = @"sensorConfig.json";
        private const string receiverConfigFileName = @"receiverConfig.json";

        public IConfigProvider<SensorConfigFile> SensorConfig { get; }
        public IConfigProvider<ReceiverConfigFile> ReceiverConfig { get; }

        public ConfigurationManager()
        {
            SensorConfig = new JsonConfigParser<SensorConfigFile>(configPath + sensorConfigFileName);
            ReceiverConfig = new JsonConfigParser<ReceiverConfigFile>(configPath + receiverConfigFileName);
        }
    }
}