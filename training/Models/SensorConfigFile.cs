using System;
using System.Collections.Generic;

namespace TelegramFLow.Models
{
    public class SensorConfigFile
    {
        public IEnumerable<SensorConfig> Sensors { get; set; } = Array.Empty<SensorConfig>();
    }
}
