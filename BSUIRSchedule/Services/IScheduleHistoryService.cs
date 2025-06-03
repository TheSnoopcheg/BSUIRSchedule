using BSUIRSchedule.Classes;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BSUIRSchedule.Services
{
    public interface IScheduleHistoryService
    {
        Task<ObservableCollection<HistoryNote>?> LoadHistoryAsync(string? path);
        Task SaveHistoryAsync(ObservableCollection<HistoryNote> history,  string? path);
    }
}
