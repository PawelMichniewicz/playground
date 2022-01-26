using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Training.Models;

namespace Training
{
    class SensorParser
    {
        private const string configFileName = "sensorConfig.json";

        public SensorParser(string path)
        {
            this.Path = path;
        }

        public string Path { get; private set; }
        
        public bool LoadConfig()
        {
            // 1. open
            // 2. parse

            // if file not exists throw
            if (string.IsNullOrWhiteSpace(Path))
            {
                throw new ArgumentNullException($"Cannot parse config file: {Path}");
            }

            try
            {
                string fullpath = Path + configFileName;
                using (var fs = new FileStream(fullpath, FileMode.Open))
                {
                    using (var fr = new StreamReader(fs))
                    {
                        using (var jtr = new JsonTextReader(fr))
                        {
                            var temp = JsonSerializer.CreateDefault();
                            var result = temp.Deserialize<SensorConfig>(jtr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException(ex.Message);
            }

            return true;
        }

        private void Parse()
        {
        }
    }
}
