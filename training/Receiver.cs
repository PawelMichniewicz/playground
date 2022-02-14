using System;
using Training.Interfaces;
using Training.Models;

namespace Training
{
    internal class Receiver : IObserver<string>
    {
        private readonly int id;
        private readonly IDecoder<Telegram> decoder;

        public IDisposable Unsubscriber { get; set; }

        public Receiver(int id, IDecoder<Telegram> decoder)
        {
            this.id = id;
            this.decoder = decoder;
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

        public void OnNext(string value)
        {
            Telegram telegram = decoder.Decode(value);
            Console.WriteLine($"Receiver #{id} got telegram:\tID: {telegram.ID}\ttype: {telegram.Type}\treading: {telegram.Reading}\tquality: {telegram.Quality}");
        }
    }
}