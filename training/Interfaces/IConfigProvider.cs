namespace Training.Interfaces
{
    public interface IConfigProvider<T> where T : class
    {
        public T LoadConfig();
    }
}