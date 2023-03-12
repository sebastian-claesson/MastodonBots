using MastodonBots.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MastodonBots
{
    public class VeckaFunction
    {
        private readonly ILogger<VeckaFunction> _logger;
        private readonly IMastodonService _mastodonService;

        public VeckaFunction(ILogger<VeckaFunction> logger, IMastodonService mastodonService)
        {
            _logger = logger;
            _mastodonService = mastodonService;
        }

        [Function(nameof(VeckaFunction))]
        public async Task Run([TimerTrigger("0 0 0 * * *", RunOnStartup = true)] TimerInfo myTimer)
        {
            _logger.LogInformation("Starting Vecka at: {Time}", DateTime.Now);

            var dateTimeService = new DateTimeService();

            var newStatus = dateTimeService.GetCurrentWeekStatus();

            var statuses = await _mastodonService.GetStatuses();

            if (!statuses.Any(x => x.Contains(newStatus)))
            {
                await _mastodonService.AddStatus(newStatus);
                _logger.LogInformation("Posted status '{Status}'", newStatus);
            }
            else
            {
                _logger.LogInformation("Status '{Status}' already existed", newStatus);
            }
        }
    }
}
