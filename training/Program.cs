using System.Threading.Tasks;

namespace Training
{
    public class Program
    {
        public static async Task Main()
        {
            await new Orchestrator().Go();
        }
    }
}
