using System.Threading.Tasks;

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
