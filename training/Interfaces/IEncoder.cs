namespace TelegramFLow.Interfaces
{
    public interface IEncoder<T>
    {
        public string Encode(T input);
    }
}