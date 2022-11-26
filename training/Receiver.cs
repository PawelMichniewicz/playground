using System;
using Training.Interfaces;
using Training.Models;
using Training.Utils;

namespace Training
{
    public class Receiver : IObserver<string>
    {
        private readonly IDecoder<Telegram> decoder;
        private readonly IPresenter presenter;

        public Receiver(ReceiverConfig config, IDecoder<Telegram> decoder, IPresenter presenter)
        {
            Config = config;
            this.decoder = decoder;
            this.presenter = presenter;
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
            presenter.Show(decoder.Decode(value));
        }
    }
}