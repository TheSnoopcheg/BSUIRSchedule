using BSUIRSchedule.Classes;
using System;
using System.Collections.ObjectModel;

namespace BSUIRSchedule.Models
{
    public interface IScheduleSearchModel
    {
        event Action ResponsesChanged;
        SearchResponse? SearchResponse { get; set; }
        ObservableCollection<SearchResponse> Responses { get; }
        string Input { get; set; }
    }
}
