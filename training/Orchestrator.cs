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
        private readonly List<SensorSimulator> simulators;
        private readonly ConfigurationManager configMgr;

        private readonly IConfigProvider<SensorConfigFile> simulatorConfig;
        private readonly IConfigProvider<ReceiverConfigFile> receiverConfig;

        private readonly IEncoder<Telegram> encoder;
        private readonly IDecoder<Telegram> decoder;

        public Orchestrator()
        {
            simulators = new();
            configMgr = new();

            simulatorConfig = configMgr.SensorConfig;
            receiverConfig = configMgr.ReceiverConfig;

            encoder = new TelegramEncoder();
            decoder = new TelegramDecoder();
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
            foreach (var sensorConfig in config.Sensors)
            {
                SensorSimulator sim = new(sensorConfig, encoder);
                sim.Start();
                simulators.Add(sim);
            }
        }

        private void RunReceivers()
        {
            var config = receiverConfig.LoadConfig();
            foreach (var recConfig in config.Receivers)
            {
                if (recConfig.Enabled)
                {
                    Receiver rx = new(recConfig, decoder);
                    rx.Unsubscriber = simulators.FirstOrDefault(x => x.Config.ID == recConfig.SimulatorID)?.Subscribe(rx);
                }
            }
        }
    }
}