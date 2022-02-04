namespace Training.Interfaces
{
    public interface IConfigProvider<T>
    {
        public T LoadConfig();
    }
}