using BSUIRSchedule.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSUIRSchedule.Services
{
    public interface IAnnouncementService
    {
        Task<List<Announcement>?> LoadAnnouncementsAsync(string? url);
    }
}
