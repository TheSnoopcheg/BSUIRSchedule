using BSUIRSchedule.Classes;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BSUIRSchedule.Models
{
    public interface IAnnouncementModel
    {
        ObservableCollection<Announcement> Announcements { get; }
        Task<bool> LoadAnnouncementsAsync(string? url);
    }
}
