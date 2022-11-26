using TelegramFLow.Interfaces;
using TelegramFLow.Models;

namespace TelegramFLow
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