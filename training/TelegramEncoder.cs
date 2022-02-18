using Training.Interfaces;
using Training.Models;

namespace Training
{
    public class TelegramEncoder : IEncoder<Telegram>
    {
        public TelegramEncoder() { }

        public string Encode(Telegram telegram)
        {
            return $"$FIX,{telegram.ID},{telegram.Type},{telegram.Reading},{telegram.Quality}*";
        }
    }
}