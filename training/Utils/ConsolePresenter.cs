using System;
using Training.Interfaces;
using Training.Models;

namespace Training.Utils
{
    public class ConsolePresenter : IPresenter
    {
        public void Show(Telegram content)
        {
            Console.ForegroundColor = ColorPicker.ColorFromQuality(content.Quality);
            Console.WriteLine($"Relegram received:\tID: {content.ID}\ttype: {content.Type}\treading: {content.Reading}\tquality: {content.Quality}");
            Console.ForegroundColor = ColorPicker.DefaultColor;
        }
    }
}
