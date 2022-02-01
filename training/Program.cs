using Training.Interfaces;
using Training.Models;

namespace Training
{
    class Program
    {
        private const string configPath = @"D:\code\localRepos\playground\training\Config\";
        private const string configName = @"sensorConfig.json";

        static void Main(string[] args)
        {
            IConfigProvider<ConfigFile> JsonConfigProvider = new JsonConfigParser<ConfigFile>(configPath + configName);
            var config = JsonConfigProvider.LoadConfig();

            RunSensors(config);

            // need smarter way to wait for Tasks to be finished
            System.Threading.Thread.Sleep(11000);

        }

        private static void RunSensors(ConfigFile config)
        {
            foreach (var sensorConfig in config.Sensors)
            {
                var sim = new SensorSimulator(sensorConfig);
                sim.Start();
            }
        }
    }
}
