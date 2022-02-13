using System.Collections.Generic;

namespace Training.Models
{
    public class ReceiverConfigFile
    {
        public ReceiverConfigFile()
        {
            Receivers = new List<int> { 1, 2, 3 };
        }

        public IEnumerable<int> Receivers { get; set; }
    }
}