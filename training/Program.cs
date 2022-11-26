using System.Threading.Tasks;

namespace TelegramFLow
{
    public class Program
    {
        public static async Task Main()
        {
            await new Orchestrator().Go();
        }
    }
}
