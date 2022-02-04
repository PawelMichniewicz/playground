using System;
using System.IO;
using Newtonsoft.Json;
using Training.Interfaces;

namespace Training
{
    class JsonConfigParser<T> : IConfigProvider<T>
    {
        private readonly string configPath;

        public JsonConfigParser(string path)
        {
            configPath = path;
        }

        public T LoadConfig()
        {
            // if file not exists throw
            if (string.IsNullOrWhiteSpace(configPath))
            {
                throw new ArgumentNullException($"Cannot parse config file: {configPath}");
            }

            T result = default;

            try
            {
                using (var fs = new FileStream(configPath, FileMode.Open))
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
