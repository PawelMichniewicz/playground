using System.Threading.Tasks;
using Training.Utils;

namespace Training
{
    public class Program
    {
        public static async Task Main()
        {
            ConfigurationManager configMgr = new();

            await new Orchestrator(configMgr.SensorConfig, configMgr.ReceiverConfig).Go();
        }
    }
}
