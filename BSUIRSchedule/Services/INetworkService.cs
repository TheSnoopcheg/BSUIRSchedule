using System.Threading.Tasks;

namespace BSUIRSchedule.Services
{
    public interface INetworkService
    {
        Task<T?> GetAsync<T>(string? url);
    }
}
