using System;
using Training.Models;

namespace Training
{
    public static class ColorPicker
    {
        public static ConsoleColor DefaultColor { get; }

        static ColorPicker()
        {
            DefaultColor = Console.ForegroundColor;
        }

        public static ConsoleColor ColorFromQuality(ReadingQuality quality)
        {
            ConsoleColor result = DefaultColor;
            switch (quality)
            {
                case ReadingQuality.Warning:
                    result = ConsoleColor.Yellow;
                    break;
                case ReadingQuality.Alarm:
                    result = ConsoleColor.Red;
                    break;
            }
            return result;
        }

    }
}