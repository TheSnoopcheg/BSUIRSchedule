using BSUIRSchedule.Classes;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BSUIRSchedule.Services
{
    public interface IFavoriteSchedulesService
    {
        Task SaveFavoriteSchedulesAsync(ObservableCollection<FavoriteSchedule> schedules);
        Task<ObservableCollection<FavoriteSchedule>> LoadFavoriteSchedulesAsync();
    }
}
