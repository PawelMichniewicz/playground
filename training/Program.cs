using System;
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
            Console.WriteLine("Hello World!");

            IConfigProvider<SensorConfig> JsonConfigProvider = new JsonConfigParser<SensorConfig>(configPath + configName);
            var config = JsonConfigProvider.LoadConfig();
        }
    }
}
