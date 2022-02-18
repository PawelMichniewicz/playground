using System;
using Training.Interfaces;
using Training.Models;

namespace Training
{
    public class TelegramDecoder : IDecoder<Telegram>
    {
        public TelegramDecoder() { }

        public Telegram Decode(string input)
        {
            Telegram result = new();

            var split = input.TrimEnd('*').Split(',');
            result.ID = Convert.ToInt32(split[1]);
            result.Type = split[2];
            result.Reading = Convert.ToInt32(split[3]);
            result.Quality = Enum.Parse<ReadingQuality>(split[4]);

            return result;
        }
    }
}