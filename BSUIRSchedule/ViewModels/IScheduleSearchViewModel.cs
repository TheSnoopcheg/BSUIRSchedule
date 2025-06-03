using BSUIRSchedule.Classes;
using System.Collections.ObjectModel;

namespace BSUIRSchedule.ViewModels
{
    public interface IScheduleSearchViewModel
    {
        SearchResponse? SearchResponse { get; set; }
        ObservableCollection<SearchResponse> Responses { get; }
        string Input { get; set; }
        void ClearInput();
    }
}
