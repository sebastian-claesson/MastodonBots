using MastodonBots.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MastodonBots
{
    public class VeckaFunction
    {
        private readonly ILogger<VeckaFunction> _logger;
        private readonly IMastodonService _mastodonService;
        private readonly DateTimeService _dateTimeService;

        public VeckaFunction(ILogger<VeckaFunction> logger, IMastodonService mastodonService)
        {
            _logger = logger;
            _mastodonService = mastodonService;
            _dateTimeService = new DateTimeService();
        }

        [Function(nameof(VeckaFunction))]
        public async Task Run([TimerTrigger("0 0 0 * * *", RunOnStartup = true)] TimerInfo myTimer)
        {
            _logger.LogInformation("Starting Vecka at: {Time}", DateTime.Now);

            var statusForCheck = _dateTimeService.GetCurrentWeekStatusForCheck();

            var statuses = await _mastodonService.GetStatuses();

            if (!statuses.Exists(x => x.Contains(statusForCheck)))
            {
                var statusForPost = _dateTimeService.GetCurrentWeekStatusForPost();
                await _mastodonService.AddStatus(statusForPost);
                _logger.LogInformation("Posted '{Status}'", statusForPost);
            }
            else
            {
                _logger.LogInformation("Status '{Status}' already existed", statusForCheck);
            }
        }
    }
}
