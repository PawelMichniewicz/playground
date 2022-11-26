using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Interfaces;
using Training.Models;
using Training.Utils;

namespace Training
{
    public class SensorSimulator : IObservable<string>
    {
        private const int milisecs = 1000;
        private const int simulationTime = 10; // seconds
        private readonly List<IObserver<string>> observers;
        private readonly IEncoder<Telegram> encoder;

        public SensorSimulator(SensorConfig config, IEncoder<Telegram> encoder)
        {
            this.encoder = encoder;
            Config = config;
            observers = new List<IObserver<string>>();
            Worker = new Task(SimulationLoop);
        }

        public SensorConfig Config { get; private set; }

        public Task Worker { get; private set; } 

        public IDisposable Subscribe(IObserver<string> observer)
        {
            observers.Add(observer);
            return new Unsubscriber<string>(observers, observer);
        }

        public void Start()
        {
            Worker.Start();
        }

        private void SimulationLoop()
        {
            int rest = milisecs / Config.Frequency;

            Random rng = new();
            Telegram telegram = new() { ID = Config.ID, Type = Config.Type };
            QualityClassifier classifier = new(Config.MinValue, Config.MaxValue);

            DateTime end = DateTime.Now.AddSeconds(simulationTime);

            while (end.TimeOfDay > DateTime.Now.TimeOfDay)
            {
                // 1. get new reading
                telegram.Reading = rng.Next(Config.MinValue, Config.MaxValue);

                // 2. decide quality
                telegram.Quality = classifier.Clasify(telegram.Reading);

                // 3. encode telegram
                string encoded = encoder.Encode(telegram);

                // 4. notify all subs
                Notify(encoded);

                // 5. wait to be inline with reading frequency
                Task.Delay(rest).Wait();
            }

            Complete();
        }

        private void Complete()
        {
            foreach (var ob in observers)
            {
                ob.OnCompleted();
            }
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