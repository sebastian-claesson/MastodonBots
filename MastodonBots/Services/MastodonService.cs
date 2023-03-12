using MastodonBots.Config;
using MastodonBots.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace MastodonBots.Services
{
    public class MastodonService : IMastodonService
    {
        private readonly ApplicationSettings _settings;
        private HttpClient _client;

        public MastodonService(IOptions<ApplicationSettings> settings)
        {
            ArgumentNullException.ThrowIfNull(settings);
            _settings = settings.Value;

            _client = new()
            {
                BaseAddress = new Uri(_settings.BaseAddress)
            };
        }


        public async Task<List<string>> GetStatuses()
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_settings.GetStatusesPath, UriKind.Relative),
                Method = HttpMethod.Get,
            };
            using var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var statuses = await response.Content.ReadFromJsonAsync<List<StatusResponse>>();

            return statuses.Select(x => x.Content).ToList();
        }

        public async Task AddStatus(string message)
        {
            var status = new StatusRequest { Status = message };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_settings.AddStatusPath, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = JsonContent.Create(status),
                Headers =
                {
                    { "Authorization", $"Bearer {_settings.ApiKey}" }
                }
            };
            using var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
