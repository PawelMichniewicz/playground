using System;
using System.IO;
using Newtonsoft.Json;
using Training.Interfaces;

namespace Training.Parsers
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
                using FileStream fs = new(configPath, FileMode.Open);
                using StreamReader sr = new(fs);
                using JsonTextReader jtr = new(sr);

                var serializer = JsonSerializer.CreateDefault();
                result = serializer.Deserialize<T>(jtr);
            }
            catch (Exception ex)
            {
                throw new FileLoadException(ex.Message);
            }

            return result;
        }

    }
}
