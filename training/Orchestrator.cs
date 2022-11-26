using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Training.Interfaces;
using Training.Models;
using Training.Utils;

namespace Training
{
    public class Orchestrator
    {
        private readonly List<SensorSimulator> simulators = new();
        private readonly ConfigurationManager configMgr = new();

        private readonly IEncoder<Telegram> encoder = new TelegramEncoder();
        private readonly IDecoder<Telegram> decoder = new TelegramDecoder();

        private readonly IConfigProvider<SensorConfigFile> simulatorConfig;
        private readonly IConfigProvider<ReceiverConfigFile> receiverConfig;

        public Orchestrator()
        {
            simulatorConfig = configMgr.SensorConfig;
            receiverConfig = configMgr.ReceiverConfig;
        }

        public async Task Go()
        {
            try
            {
                RunSensors();
                RunReceivers();
                await Task.WhenAll(simulators.Select(x => x.Worker));
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Operation failed unexpectedly! Message: {ex.Message}");
            }
        }

        private void RunSensors()
        {
            var config = simulatorConfig.LoadConfig();
            foreach (var sim in config.Sensors.Select(c => new SensorSimulator(c, encoder)))
            {
                sim.Start();
                simulators.Add(sim);
            }
        }

        private void RunReceivers()
        {
            var config = receiverConfig.LoadConfig();
            foreach (var rx in config.Receivers.Where(c => c.Enabled).Select(c => new Receiver(c, decoder)))
            {
                rx.Unsubscriber = simulators.First(x => x.Config.ID == rx.Config.SimulatorID).Subscribe(rx);
            }
        }
    }
}