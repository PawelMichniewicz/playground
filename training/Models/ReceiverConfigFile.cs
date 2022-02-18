using System.Collections.Generic;

namespace Training.Models
{
    public class ReceiverConfigFile
    {
        public ReceiverConfigFile() { }

        public IEnumerable<ReceiverConfig> Receivers { get; set; }
    }
}