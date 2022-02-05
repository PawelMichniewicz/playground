using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training
{
    internal class SensorSimulator : IObservable<string>
    {
        private const int milisecs = 1000;
        private readonly List<IObserver<string>> observers;

        public SensorConfig SensorConfig { get; private set; }

        public Task Worker { get; private set; }

        public SensorSimulator(SensorConfig sensorConfig)
        {
            this.SensorConfig = sensorConfig;
            observers = new List<IObserver<string>>();
            Setup();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            observers.Add(observer);
            return new Unsubscriber<string>(observers, observer);
        }

        public void Start()
        {
            Worker.Start();
            //return worker;
        }

        private void Setup()
        {
            Worker = new Task(() =>
            {
                int rest = milisecs / SensorConfig.Frequency;
                int reading;
                string telegram = string.Empty;
                Random chaos = new();
                QualityClassifier classifier = new(SensorConfig.MinValue, SensorConfig.MaxValue);
                QualityClassifier.ReadingQuality quality;
                TelegramEncoder encoder = new(SensorConfig);

                DateTime endTime = DateTime.Now.AddSeconds(10);

                while (endTime > DateTime.Now)
                {
                    // 1. get new reading
                    reading = chaos.Next(SensorConfig.MinValue, SensorConfig.MaxValue);

                    // 2. decide quality based on new reading
                    quality = classifier.Clasify(reading);

                    // 3. encode new telegram
                    telegram = encoder.Encode(reading, quality);

                    // 4. notify all subs
                    Notify(telegram);

                    // 5. wait to be inline with reading frequency
                    System.Threading.Thread.Sleep(rest);
                }

                // notify complete()
            });
        }

        private void Notify(string telegram)
        {
            foreach (var ob in observers)
            {
                ob.OnNext(telegram);
            }
        }




        private class Unsubscriber<T> : IDisposable
        {
            private readonly List<IObserver<T>> observers;
            private readonly IObserver<T> observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observer != null && observers.Contains(observer))
                {
                    observers.Remove(observer);
                }
            }
        }
    }
}