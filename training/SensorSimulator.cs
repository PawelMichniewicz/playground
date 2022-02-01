using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training
{
    internal class SensorSimulator : IObservable<int>
    {
        private const int milisecs = 1000;

        private SensorConfig sensorConfig;

        private Task worker;

        private List<IObserver<int>> observers;

        public SensorSimulator(SensorConfig sensorConfig)
        {
            this.sensorConfig = sensorConfig;
            observers = new List<IObserver<int>>();
            Setup();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        // maybe return Task?
        public void Start() => worker.Start();

        private void Setup()
        {
            worker = new Task(() =>
            {
                var rest = milisecs / sensorConfig.Frequency;
                var chaos = new Random();

                var endTime = DateTime.Now.AddSeconds(10);

                while (endTime > DateTime.Now)
                {
                    // 1. get new reading
                    int reading = chaos.Next(sensorConfig.MinValue, sensorConfig.MaxValue);

                    // 2. decide on reading quality here based on new reading

                    // 3. construct message string
                    Console.WriteLine($"ID: {sensorConfig.ID}\tType: {sensorConfig.Type}\tFreq: {sensorConfig.Frequency} Hz\tReading: {reading}");
                    
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