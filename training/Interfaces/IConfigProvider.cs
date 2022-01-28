namespace Training.Interfaces
{
    public interface IConfigProvider<T>
    {
        public string ConfigPath { get; set; }

        public T LoadConfig();
    }
}