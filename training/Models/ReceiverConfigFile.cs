using System;
using System.Collections.Generic;

namespace TelegramFLow.Models
{
    public class ReceiverConfigFile
    {
        public IEnumerable<ReceiverConfig> Receivers { get; set; } = Array.Empty<ReceiverConfig>();
    }
}