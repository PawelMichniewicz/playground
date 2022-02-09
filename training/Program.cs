using System.Threading.Tasks;

namespace Training
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConfigurationManager configMgr = new();

            await new Orchestrator(configMgr.SensorConfig, configMgr.ReceiverConfig).Go();
        }
    }
}
