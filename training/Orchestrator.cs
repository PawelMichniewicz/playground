using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Interfaces;
using Training.Models;

namespace Training
{
    internal class Orchestrator
    {
        private readonly List<SensorSimulator> simulators = new();
        private readonly List<Receiver> receivers = new();

        private readonly IConfigProvider<SensorConfigFile> simulatorConfig;
        private readonly IConfigProvider<ReceiverConfigFile> receiverConfig;

        public Orchestrator(IConfigProvider<SensorConfigFile> simConfig, IConfigProvider<ReceiverConfigFile> rxConfig)
        {
            simulatorConfig = simConfig;
            receiverConfig = rxConfig;
        }

        public async Task Go()
        {
            RunSensors(simulatorConfig.LoadConfig());
            
            RunReceivers(receiverConfig.LoadConfig());

            await Task.WhenAll(simulators.Select(x => x.Worker));

            UnsubscribeReceivers();
        }

        private void UnsubscribeReceivers()
        {
            foreach (var rx in receivers)
            {
                rx.UnsubscribeMe();
            }
        }

        private void RunSensors(SensorConfigFile config)
        {
            foreach (var sensorConfig in config.Sensors)
            {
                var sim = new SensorSimulator(sensorConfig);
                sim.Start();
                simulators.Add(sim);
            }
        }
        
        private void RunReceivers(ReceiverConfigFile config)
        {
            foreach (var id in config.Receivers)
            {
                var rx = new Receiver(id);
                rx.Unsubscriber = simulators.FirstOrDefault(x => x.SensorConfig.ID == id)?.Subscribe(rx);
                receivers.Add(rx);
            }
        }
    }
}