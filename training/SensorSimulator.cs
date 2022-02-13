﻿using System;
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
            int reading;
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
                string telegram = encoder.Encode(reading, quality);

                // 4. notify all subs
                Notify(telegram);

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