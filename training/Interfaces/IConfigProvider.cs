namespace TelegramFLow.Interfaces
{
    public interface IConfigProvider<T> where T : class
    {
        public T LoadConfig();
    }
}