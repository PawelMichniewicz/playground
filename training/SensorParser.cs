using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training
{
    class SensorParser
    {
        public SensorParser(string path)
        {
            this.Path = path;
        }

        public string Path { get; private set; }
        
        public bool LoadConfig()
        {
            // if file not exists throw
            if (string.IsNullOrWhiteSpace(Path))
            {
                throw new ArgumentNullException($"Cannot parse config file: {Path}");
            }
            // else: open file under path

            return true;
        }

        public void Parse()
        {
        }
    }
}
