using System;
using System.Collections.Generic;

namespace Training.Models
{
    public class SensorConfigFile
    {
        public IEnumerable<SensorConfig> Sensors { get; set; } = Array.Empty<SensorConfig>();
    }
}
