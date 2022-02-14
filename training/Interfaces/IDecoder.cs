namespace Training.Interfaces
{
    public interface IDecoder<T>
    {
        public T Decode(string input);
    }
}