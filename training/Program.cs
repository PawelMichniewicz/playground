using System;
using System.Threading.Tasks;
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
            IConfigProvider<SensorConfig> JsonConfigProvider = new JsonConfigParser<SensorConfig>(configPath + configName);
            var config = JsonConfigProvider.LoadConfig();

            RunSensors(config);

            System.Threading.Thread.Sleep(11000);

        }

        private static void RunSensors(SensorConfig config)
        {
            const int milisecs = 1000;

            foreach (var sensor in config.Sensors)
            {
                new Task(() =>
                {
                    var rest = milisecs / sensor.Frequency;
                    var chaos = new Random();

                    var endTime = DateTime.Now.AddSeconds(10);

                    while (endTime > DateTime.Now)
                    {
                        int reading = chaos.Next(sensor.MinValue, sensor.MaxValue);
                        Console.WriteLine($"ID: {sensor.ID}\tType: {sensor.Type}\tFreq: {sensor.Frequency} Hz\tReading: {reading}");
                        System.Threading.Thread.Sleep(rest);
                    }
                }).Start();
            }
        }
    }
}
