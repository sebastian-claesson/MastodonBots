using MastodonBots.Config;
using MastodonBots.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MastodonBots
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(s =>
                {
                    s.AddSingleton<IMastodonService, MastodonService>();

                    s.AddOptions<ApplicationSettings>().Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection(nameof(ApplicationSettings)).Bind(settings);
                    });
                })
                .Build();
            await host.RunAsync();
        }
    }
}
