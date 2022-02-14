using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Interfaces;
using Training.Models;

namespace Training
{
    internal class SensorSimulator : IObservable<string>
    {
        private const int milisecs = 1000;
        private const int simulationTime = 10; // seconds
        private readonly List<IObserver<string>> observers;
        private readonly IEncoder<Telegram> encoder;

        public SensorConfig SensorConfig { get; private set; }

        public Task Worker { get; private set; }

        public SensorSimulator(SensorConfig sensorConfig, IEncoder<Telegram> encoder)
        {
            this.SensorConfig = sensorConfig;
            this.encoder = encoder;
            observers = new List<IObserver<string>>();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            observers.Add(observer);
            return new Unsubscriber<string>(observers, observer);
        }

        public void Start()
        {
            (Worker ??= new Task(SimulationLoop)).Start();
        }

        private void SimulationLoop()
        {
            int rest = milisecs / SensorConfig.Frequency;

            Random chaos = new();
            Telegram telegram = new() { ID = SensorConfig.ID, Type = SensorConfig.Type };
            QualityClassifier classifier = new(SensorConfig.MinValue, SensorConfig.MaxValue);

            DateTime endTime = DateTime.Now.AddSeconds(simulationTime);

            while (endTime.TimeOfDay > DateTime.Now.TimeOfDay)
            {
                // 1. get new reading
                telegram.Reading = chaos.Next(SensorConfig.MinValue, SensorConfig.MaxValue);

                // 2. decide quality based on new reading
                telegram.Quality = classifier.Clasify(telegram.Reading);

                // 3. encode new telegram
                string encoded = encoder.Encode(telegram);

                // 4. notify all subs
                Notify(encoded);

                // 5. wait to be inline with reading frequency
                System.Threading.Thread.Sleep(rest);
            }

            // notify complete()
            //Complete();
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