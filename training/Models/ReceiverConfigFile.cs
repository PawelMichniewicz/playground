using System;
using System.Collections.Generic;

namespace Training.Models
{
    public class ReceiverConfigFile
    {
        public IEnumerable<ReceiverConfig> Receivers { get; set; } = Array.Empty<ReceiverConfig>();
    }
}