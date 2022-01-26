using System;

namespace Training
{
    class Program
    {
        private const string ConfigPath = @"D:\code\localRepos\playground\training\Config\";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var parser = new SensorParser(ConfigPath);
            parser.LoadConfig();
        }
    }
}
