using Training.Interfaces;
using Training.Models;
using Training.Parsers;

namespace Training
{
    public class ConfigurationManager
    {
        private const string configPath = @".\..\..\..\Config\";
        private const string sensorConfigFileName = @"sensorConfig.json";
        private const string receiverConfigFileName = @"receiverConfig.json";

        public IConfigProvider<SensorConfigFile> SensorConfig { get; private set; }
        public IConfigProvider<ReceiverConfigFile> ReceiverConfig { get; private set; }

        public ConfigurationManager()
        {
            //System.Environment.GetEnvironmentVariable()
            SensorConfig = new JsonConfigParser<SensorConfigFile>(configPath + sensorConfigFileName);
            ReceiverConfig = new JsonConfigParser<ReceiverConfigFile>(configPath + receiverConfigFileName);
        }
    }
}