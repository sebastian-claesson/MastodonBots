using System.Globalization;

namespace MastodonBots.Services
{
    internal class DateTimeService
    {
        private readonly string _Text = "It is now week";

        public string GetCurrentWeekStatusForCheck() => $"{_Text} {GetCurrentWeek()}";

        public string GetCurrentWeekStatusForPost() => $"{_Text} {GetCurrentWeek()}\n\n#hachybots";

        private int GetCurrentWeek() => ISOWeek.GetWeekOfYear(DateTime.UtcNow);
    }
}
