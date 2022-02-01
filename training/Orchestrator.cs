using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Interfaces;
using Training.Models;

namespace Training
{
    internal class Orchestrator
    {
        private const string configPath = @"D:\code\localRepos\playground\training\Config\";
        private const string configName = @"sensorConfig.json";

        private List<Task> simulators = new List<Task>();

        public Orchestrator() { }

        internal void Go()
        {
            IConfigProvider<ConfigFile> JsonConfigProvider = new JsonConfigParser<ConfigFile>(configPath + configName);
            var config = JsonConfigProvider.LoadConfig();

            RunSensors(config);

            int timeout = 20000; // 20s
            //Task.WhenAll(simulators);
            Task.WaitAll(simulators.ToArray(), timeout);
        }

        private void RunSensors(ConfigFile config)
        {
            foreach (var sensorConfig in config.Sensors)
            {
                var sim = new SensorSimulator(sensorConfig);
                simulators.Add(sim.Start());
            }
        }
    }
}