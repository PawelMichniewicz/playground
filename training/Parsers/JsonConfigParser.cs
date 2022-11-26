using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using Training.Interfaces;

namespace Training.Parsers
{
    public class JsonConfigParser<T> : IConfigProvider<T> where T : class
    {
        private readonly string configPath;
        private T? config;

        public JsonConfigParser(string path)
        {
            configPath = path;
            config = null;
        }

        public T LoadConfig()
        {
            return LoadConfig(forceReload: false);
        }

        public T LoadConfig(bool forceReload)
        {
            try
            {
                if (forceReload || null == config)
                {
                    string jsonString = File.ReadAllText(configPath);

                    JsonSerializerOptions options = new()
                    {
                        ReadCommentHandling = JsonCommentHandling.Skip
                    };

                    config = JsonSerializer.Deserialize<T>(jsonString, options) ?? throw new SerializationException();
                }
            }
            catch (Exception)
            {
                throw new FileLoadException($"Loading configuration file <<{configPath}>> failed.");
            }

            return config;
        }

    }
}
