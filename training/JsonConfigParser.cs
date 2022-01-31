using System;
using System.IO;
using Newtonsoft.Json;
using Training.Interfaces;

namespace Training
{
    class JsonConfigParser<T> : IConfigProvider<T>
    {
        public JsonConfigParser(string path)
        {
            ConfigPath = path;
        }

        public string ConfigPath { get;  set; }
        
        public T LoadConfig()
        {
            // if file not exists throw
            if (string.IsNullOrWhiteSpace(ConfigPath))
            {
                throw new ArgumentNullException($"Cannot parse config file: {ConfigPath}");
            }

            T result = default;

            try
            {
                using (var fs = new FileStream(ConfigPath, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        using (var jtr = new JsonTextReader(sr))
                        {
                            var serializer = JsonSerializer.CreateDefault();
                            result = serializer.Deserialize<T>(jtr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException(ex.Message);
            }

            return result;
        }

    }
}
