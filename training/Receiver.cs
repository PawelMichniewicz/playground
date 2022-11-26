using System;
using Training.Interfaces;
using Training.Models;
using Training.Utils;

namespace Training
{
    public class Receiver : IObserver<string>
    {
        private readonly IDecoder<Telegram> decoder;

        public Receiver(ReceiverConfig config, IDecoder<Telegram> decoder)
        {
            Config = config;
            this.decoder = decoder;
        }

        public IDisposable? Unsubscriber { get; set; }

        public ReceiverConfig Config { get; private set; }

        private void UnsubscribeMe() => Unsubscriber?.Dispose();

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

            Console.ForegroundColor = ColorPicker.ColorFromQuality(telegram.Quality);
            Console.WriteLine($"Receiver #{Config.ID} got telegram:\tID: {telegram.ID}\ttype: {telegram.Type}\treading: {telegram.Reading}\tquality: {telegram.Quality}");
            Console.ForegroundColor = ColorPicker.DefaultColor;
        }
    }
}