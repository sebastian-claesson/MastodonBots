namespace MastodonBots.Services
{
    public interface IMastodonService
    {
        Task AddStatus(string message);
        Task<List<string>> GetStatuses();
    }
}