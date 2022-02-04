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
            UnsubscribeMe();
        }

        public void OnError(Exception error)
        {
            UnsubscribeMe();
        }

        public void OnNext(int value)
        {
            Console.WriteLine($"Receiver #{id} got value: {value}");
        }
    }
}