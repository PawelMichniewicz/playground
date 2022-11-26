using System;
using TelegramFLow.Models;

namespace TelegramFLow.Utils
{
    public class QualityClassifier
    {
        private readonly int leftAlarmBreakpoint;
        private readonly int rightAlarmBreakpoint;
        private readonly int leftWarningBreakpoint;
        private readonly int rightWarningBreakpoint;

        public QualityClassifier(int minValue, int maxValue)
        {
            int range = Math.Abs(minValue) + Math.Abs(maxValue);
            leftAlarmBreakpoint = (int)(minValue + range * 0.1);
            rightAlarmBreakpoint = (int)(maxValue - range * 0.1);
            leftWarningBreakpoint = (int)(minValue + range * 0.25);
            rightWarningBreakpoint = (int)(maxValue - range * 0.25);
        }

        public ReadingQuality Clasify(int reading)
        {
            ReadingQuality result = ReadingQuality.Normal;
            if (reading < leftWarningBreakpoint || reading > rightWarningBreakpoint)
            {
                result = ReadingQuality.Warning;
                if (reading < leftAlarmBreakpoint || reading > rightAlarmBreakpoint)
                {
                    result = ReadingQuality.Alarm;
                }
            }

            return result;
        }
    }
}