using System.Globalization;

namespace MastodonBots.Services
{
    internal class DateTimeService
    {
        public string GetCurrentWeekStatus()
        {
            return $"It is now week {GetCurrentWeek()}\n\n#hachybots";
        }

        private int GetCurrentWeek()
        {
            return ISOWeek.GetWeekOfYear(DateTime.UtcNow);
        }
    }
}
