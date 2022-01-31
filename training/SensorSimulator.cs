using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.Models;

namespace Training
{
    internal class SensorSimulator : IObservable<int>, IDisposable
    {
        private const int milisecs = 1000;

        private Sensor sensorConfig;

        private Task worker;

        private List<IObserver<int>> observers;

        public SensorSimulator(Sensor sensorConfig)
        {
            this.sensorConfig = sensorConfig;
            observers = new List<IObserver<int>>();
            Setup();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            observers.Add(observer);
            return this;
        }

        public void Dispose()
        {
            //unsubsribe here
            // need to rethink this...
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
                    int reading = chaos.Next(sensorConfig.MinValue, sensorConfig.MaxValue);
                    Console.WriteLine($"ID: {sensorConfig.ID}\tType: {sensorConfig.Type}\tFreq: {sensorConfig.Frequency} Hz\tReading: {reading}");
                    // notify all subs
                    Notify(reading);
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
    }
}