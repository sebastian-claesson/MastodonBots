using System;
using System.Globalization;

namespace MastodonBots.Services
{
    internal class DateTimeService
    {
        public string GetCurrentWeekStatus()
        {
            return $"Det är nu vecka {GetCurrentWeek()}";
        }

        private int GetCurrentWeek()
        {
            return ISOWeek.GetWeekOfYear(DateTime.UtcNow);
        }
    }
}
