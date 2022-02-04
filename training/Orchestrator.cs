using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Interfaces;
using Training.Models;

namespace Training
{
    internal class Orchestrator
    {
        private const string configPath = @".\..\..\..\Config\";
        private const string sensorConfigFileName = @"sensorConfig.json";

        private readonly List<SensorSimulator> simulators = new();
        private readonly List<Receiver> receivers = new();

        public Orchestrator() { }

        internal void Go()
        {
            IConfigProvider<ConfigFile> JsonConfigProvider = new JsonConfigParser<ConfigFile>(configPath + sensorConfigFileName);
            ConfigFile config = JsonConfigProvider.LoadConfig();

            RunSensors(config);

            // load receiver config here
            RunReceivers();

            int timeout = 20000; // 20s
            // await Task.WhenAll(simulators.Select(x => x.Worker)); // <-- this needs some work
            Task.WaitAll(simulators.Select(x => x.Worker).ToArray(), timeout);

            UnsubscribeReceivers();
        }

        private void UnsubscribeReceivers()
        {
            foreach (var rx in receivers)
            {
                rx.UnsubscribeMe();
            }
        }

        private void RunReceivers()
        {
            List<int> temp = new() { 1/*, 2, 3 */};

            foreach (var id in temp)
            {
                var rx = new Receiver(id);
                rx.Unsubscriber = simulators.FirstOrDefault(x => x.SensorConfig.ID == id).Subscribe(rx);
                receivers.Add(rx);
            }
        }

        private void RunSensors(ConfigFile config)
        {
            foreach (var sensorConfig in config.Sensors)
            {
                var sim = new SensorSimulator(sensorConfig);
                sim.Start();
                simulators.Add(sim);
            }
        }
    }
}