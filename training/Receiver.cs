using System;

namespace Training
{
    internal class Receiver : IObserver<int>
    {
        private readonly int id;

        public IDisposable Unsubscriber { get; set; }

        public Receiver(int id)
        {
            this.id = id;
        }

        public void UnsubscribeMe() => Unsubscriber?.Dispose();

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(int value)
        {
            Console.WriteLine($"Receiver #{id} got value: {value}");
        }
    }
}