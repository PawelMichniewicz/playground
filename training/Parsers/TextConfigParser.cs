using Training.Interfaces;
using Training.Models;

namespace Training.Parsers
{
    public class TextConfigParser<T> : IConfigProvider<T> where T : ReceiverConfigFile
    {
        private readonly string configPath;

        public TextConfigParser(string path)
        {
            configPath = path;
        }

        public T LoadConfig()
        {
            T result = (T)new ReceiverConfigFile();
            return result;
        }
    }
}