using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training
{
    internal class SensorSimulator : IObservable<int>
    {
        private const int milisecs = 1000;
        private readonly List<IObserver<int>> observers;

        public SensorConfig SensorConfig { get; private set; }

        public Task Worker { get; private set; }

        public SensorSimulator(SensorConfig sensorConfig)
        {
            this.SensorConfig = sensorConfig;
            observers = new List<IObserver<int>>();
            Setup();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            observers.Add(observer);
            return new Unsubscriber(observers, observer);
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
                Random chaos = new();
                QualityClassifier classifier = new(SensorConfig.MinValue, SensorConfig.MaxValue);
                QualityClassifier.ReadingQuality quality;

                DateTime endTime = DateTime.Now.AddSeconds(10);

                while (endTime > DateTime.Now)
                {
                    // 1. get new reading
                    reading = chaos.Next(SensorConfig.MinValue, SensorConfig.MaxValue);

                    // 2. decide on reading quality based on new reading
                    quality = classifier.Clasify(reading);

                    // 3. encode new telegram
                    //Console.WriteLine($"ID: {SensorConfig.ID}\tType: {SensorConfig.Type}\tFreq: {SensorConfig.Frequency} Hz\tReading: {reading}");

                    // 4. notify all subs
                    Notify(reading);

                    // 5. wait to be inline with reading frequency
                    System.Threading.Thread.Sleep(rest);
                }
            });
        }

        private void Notify(int reading)
        {
            foreach (var ob in observers)
            {
                ob.OnNext(reading);
            }
        }




        private class Unsubscriber : IDisposable
        {
            private List<IObserver<int>> observers;
            private IObserver<int> observer;

            public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
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