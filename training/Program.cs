using System.Threading.Tasks;

namespace Training
{
    class Program
    {
        static async Task Main()
        {
            ConfigurationManager configMgr = new();

            await new Orchestrator(configMgr.SensorConfig, configMgr.ReceiverConfig).Go();
        }
    }
}
